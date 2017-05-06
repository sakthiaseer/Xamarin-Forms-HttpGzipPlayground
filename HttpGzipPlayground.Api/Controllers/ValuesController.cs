using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpGzipPlayground.Api.Controllers
{
    public class ValuesController : Controller
    {
        public JsonResult Index() => new JsonResult(GenerateDummyData());

        private List<Dummy> GenerateDummyData()
        {
            var result = new List<Dummy>();
            
            for(int i=0;i<100;i++)
            {
                result.Add(new Dummy{ Id = Guid.NewGuid().ToString() });
            }

            return result;
        }
    }

    public class Dummy
    {
        public string Id {get;set;}   
    }
}
