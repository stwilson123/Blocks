using System;

namespace Blocks.Framework.Test.Authorization.Permission
{
    public class PermissionTest
    {
        public void EnumCalTest(int enum1)
        {
          
        }

        public void set()
        {
            var a = permissionDefault.Add;
          //  EnumCalTest(a);
        }
    }

    public enum permissionDefault
    {
        Add,
        Edit,
        Delete
    }
    public enum permission : byte
    {
        Add,
        Edit,
        Delete
    }
}