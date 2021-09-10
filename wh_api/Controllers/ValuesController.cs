using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace wh_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static string resource = "D:/users.log";
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(GetActive());
            //
          //  return Ok(JsonConvert.SerializeObject(System.IO.File.ReadAllLines(resource)));
          //  return new string[] { "value1", "value2" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        [HttpPost]
        public IActionResult Post(JObject value)
        {
            return Ok(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        protected string GetActive()
        {
            DataTable dtstatus = new DataTable();
            dtstatus.Columns.Add("PortType");
            dtstatus.Columns.Add("HostIP");
            dtstatus.Columns.Add("RemoteIP");
            dtstatus.Columns.Add("Status");
            var act = System.IO.File.ReadAllLines(resource);
            if (act != null)
            {
                int ski = 0;
                foreach (var s in act)
                {
                    ski++;
                    if (ski > 4)
                        //if (!s.Contains("Proto"))
                        //{
                        if (!s.Contains("127.0.0.1") && !s.Contains("TIME_WAIT"))
                        {
                            var prt = "";
                            var hst = "";
                            var rmt = "";
                            var sts = "";
                            try
                            {
                                var enc = Encoding.UTF8.GetString(Encoding.Default.GetBytes(s));
                                prt = enc.Substring(0, 8);
                                hst = enc.Replace(prt, "").TrimStart().Split(' ').First(); //enc.Substring(8, 22);
                                rmt = enc.Replace(prt, "").Replace(hst, "").TrimStart().Split(' ').First(); //enc.Substring(22, 21);
                                sts = enc.TrimEnd().Split(' ').Last();  // prt.Replace(prt, "").Replace(hst, "").Replace(rmt, "").Trim();
                            }
                            catch
                            {
                               // Response.Write("Error In reading");
                            }
                            dtstatus.Rows.Add(new object[] { prt, hst, rmt, sts });
                        }
                    //}
                }
            }
            return DataTableToJSONWithJSONNet(dtstatus);
        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
