using System;
using System.Collections.Generic;
using System.Text;

namespace EC.Domain.Guards
{
    public class Guard : IGuardService
    {
        public static IGuardService Instance { get; } = new Guard();
        private Guard()
        {

        }
    }
}
