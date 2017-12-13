using System;

namespace Blocks.Framework.Exceptions.Helper
{
    public static class ExceptionHelper
    {
        public static void ThrowArgumentNullException(ExceptionArgument argument)
        {
            throw new ArgumentNullException(ExceptionHelper.GetArgumentName(argument));
        }


        internal static string GetArgumentName(ExceptionArgument argument)
        {
            switch (argument)
            {
                case ExceptionArgument.obj:
                    return "obj";
                case ExceptionArgument.dictionary:
                    return "dictionary";
                case ExceptionArgument.dictionaryCreationThreshold:
                    return "dictionaryCreationThreshold";
                case ExceptionArgument.array:
                    return "array";
                case ExceptionArgument.info:
                    return "info";
                case ExceptionArgument.key:
                    return "key";
                case ExceptionArgument.collection:
                    return "collection";
                case ExceptionArgument.list:
                    return "list";
                case ExceptionArgument.match:
                    return "match";
                case ExceptionArgument.converter:
                    return "converter";
                case ExceptionArgument.queue:
                    return "queue";
                case ExceptionArgument.stack:
                    return "stack";
                case ExceptionArgument.capacity:
                    return "capacity";
                case ExceptionArgument.index:
                    return "index";
                case ExceptionArgument.startIndex:
                    return "startIndex";
                case ExceptionArgument.value:
                    return "value";
                case ExceptionArgument.count:
                    return "count";
                case ExceptionArgument.arrayIndex:
                    return "arrayIndex";
                case ExceptionArgument.name:
                    return "name";
                case ExceptionArgument.mode:
                    return "mode";
                case ExceptionArgument.item:
                    return "item";
                case ExceptionArgument.options:
                    return "options";
                case ExceptionArgument.view:
                    return "view";
                case ExceptionArgument.sourceBytesToCopy:
                    return "sourceBytesToCopy";
                default:
                    return string.Empty;
            }
        }
    }

    public enum ExceptionArgument
    {
        obj,
        dictionary,
        dictionaryCreationThreshold,
        array,
        info,
        key,
        collection,
        list,
        match,
        converter,
        queue,
        stack,
        capacity,
        index,
        startIndex,
        value,
        count,
        arrayIndex,
        name,
        mode,
        item,
        options,
        view,
        sourceBytesToCopy,
    }
}