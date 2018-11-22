using System;

namespace Blocks.Framework.Security.Authorization.Permission.Attributes
{
    /// <summary>
    /// This attribute is used on a method of an Application Service (A class that implements <see cref="IAppService"/>)
    /// to make that method usable only by authorized users.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BlocksAuthorizeAttribute : Attribute
    {
        /// <summary>
        /// A list of permissions to authorize.
        /// </summary>
        public string[] Permissions { get; }


        /// <summary>
        /// Creates a new instance of <see cref="BlocksAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        /// <param name="actions">Current services has actions<</param>
        public BlocksAuthorizeAttribute(params string[] permission)
        {
            Permissions = permission;
        }
        
         
    }
}