using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using policymicroservice.Models;
using policymicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace policymicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class policyController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        
        public policyController()
        {
           
            _log4net = log4net.LogManager.GetLogger(typeof(policyController));
        }
        // GET: api/<memberController>
        [HttpGet]
        public IActionResult Get()
        {
            memberpolicyrepo ob = new memberpolicyrepo();
            return Ok(ob.fun());
        }
        [HttpGet("{memberid}")]
        public int getpolid(int memberid)
        {
            memberpolicyrepo ob = new memberpolicyrepo();
            int policyid = ob.receivepolicyid(memberid);
            return policyid;
        }
        [HttpGet("{policyid}/{memberid}")]
        public List<int> GetEligibleBenefits(int policyid, int memberid)//run in browser1 2 100 200 300 400
        {
            try
            {

                _log4net.Info("policyController get eligible benefits");
                memberpolicyrepo o = new memberpolicyrepo();
                providerpolicyrepo res = new providerpolicyrepo();
                List<int> cob = new List<int>();
                List<int> f = new List<int>();
                cob = o.gethosloc(memberid); //hosp loc
                f = res.getbenefits(cob);
                return f;
            }
            catch
            {
                List<int> tk = new List<int>();
                return tk;
            }
            
            
        }
        [HttpGet("{policyid}/{memberid}/{benefitid}")]



        // ambulance medical operation staying
        //    1         2        3         4      ----> for user
        //    0         1        2         3      ----> as per my list
        public int getEligibleClaimAmount(int policyid,int memberid,int benefitid)
        {
            try
            {
                _log4net.Info("policyController get eligible claim ammount");
                if (benefitid == 0)
                {
                    policyrepo x = new policyrepo();
                    int y = x.givepolicy(policyid);
                    return 2 * y;
                }
                else
                {
                    List<int> r = new List<int>();
                    r = GetEligibleBenefits(policyid, memberid);//[{ 400,300,200,300}]
                    return r[benefitid - 1];

                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
