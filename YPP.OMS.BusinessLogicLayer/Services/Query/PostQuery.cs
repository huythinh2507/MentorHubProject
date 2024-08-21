using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.Services.Query
{
    public class PostQuery
    {
        public string HelloWorld => "Hello, world!";

        public Post GetPost(int id)
        {
            return new Post { Id = id, Title = "Sample Post" };
        }
    }
}
