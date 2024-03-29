﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Abp.Aspects;
using Abp.Dependency;
using Blocks.Framework.Exceptions;
using Blocks.Framework.Localization;
using Blocks.Framework.RPCProxy;
using Blocks.Framework.Threading;
using Castle.DynamicProxy;

namespace Blocks.Framework.Auditing
{
    internal class AuditingInterceptor : IInterceptor
    {
        private readonly IAuditingHelper _auditingHelper;
        protected ILocalizationContext _localizationContext { get; }
        public AuditingInterceptor(IAuditingHelper auditingHelper, ILocalizationContext localizationContext)
        {
            _auditingHelper = auditingHelper;
            _localizationContext = localizationContext;
        }

        public void Intercept(IInvocation invocation)
        {
            if (AbpCrossCuttingConcerns.IsApplied(invocation.InvocationTarget, AbpCrossCuttingConcerns.Auditing))
            {
                invocation.Proceed();
                return;
            }

            if (!_auditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            var auditInfo = _auditingHelper.CreateAuditInfo(invocation.TargetType, invocation.MethodInvocationTarget, invocation.Arguments);

            if (invocation.Method.IsAsync())
            {
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                PerformSyncAuditing(invocation, auditInfo);
            }
        }

        private void PerformSyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                //auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

                SaveAuditInfo(auditInfo, stopwatch,  auditInfo.Exception, invocation.ReturnValue);
                // _auditingHelper.Save(auditInfo);
            }
        }

        private void PerformAsyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task) invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception,null)
                    );
            }
            else //Task<TResult>
            {
                 invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    (result,exception) => SaveAuditInfo(auditInfo, stopwatch, exception,result)
                    );
            }
        }

        private void SaveAuditInfo(AuditInfo auditInfo, Stopwatch stopwatch, Exception exception,object returnValue)
        {
            stopwatch.Stop();
            auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
    
            
            _auditingHelper.UpdateAuditInfo(auditInfo, exception, genReturnValue(exception, returnValue));
            auditInfo.SystemException = auditInfo.Exception is BlocksException ? null: auditInfo.Exception;
            _auditingHelper.Save(auditInfo);
        }

        private object genReturnValue(Exception exception,object returnValue)
        {
            var bEx = exception is BlocksException ? (BlocksException) exception: null;

            if (bEx == null)
                return new DataResult()
                {
                    code = ResultCode.Success,
                    content = returnValue,
                    //   msg = string.Format(bEx?.Message.FormatStr,bEx?.Message.FormatArgs),
                };
            return new DataResult()
            {
                code = bEx?.Code ?? ResultCode.Fail,
                content = bEx?.Content,
                msg = bEx?.Message?.ToString() ??
                      bEx?.LMessage?.Localize(_localizationContext) ?? exception.Message,
            };
        }
    }
    
    public static class ResultCode
    {
        public static readonly string Success = "200";
        
        
        public static readonly string Fail = "101";

    }
}