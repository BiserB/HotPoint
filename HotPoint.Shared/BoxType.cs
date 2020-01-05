using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Shared
{
    public class BoxType : Enumeration
    {
        public static readonly BoxType Small = new BoxType(1, "малка");
        public static readonly BoxType Big = new BoxType(2, "голяма");

        public BoxType(int id, string name) : base(id, name)
        {
        }
    }
}
