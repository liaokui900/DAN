using System;
using System.Collections.Generic;
using System.Text;

namespace DAN.Tests
{
    public interface IRepository<TDocument>
    {
        bool Update(User user);
    }
}
