using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogic
{
    [Flags]

    public enum Genre
    {
        Romance = 1,
        Fantasy = 2,
        Mystery = 4,
        Horror = 8,
        Adventure = 16,
        Historical = 32,
        Drama = 64,
        Comedy = 128,
        Crime = 256,
        NonFiction = 512,
        YoungAdult = 1024,
        Children = 2048,
        Poetry = 4096,
        Sports = 8192,
    }
}
