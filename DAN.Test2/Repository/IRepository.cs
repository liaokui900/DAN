using System;
using System.Collections.Generic;
using System.Text;

namespace DAN.Test2
{
    public interface IRepository<TDocument>
    {
        bool Update(User user);
    }
}
