using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.WebUI.Store;
using Newtonsoft.Json;

namespace WebApp.WebUI.Controllers
{
    /// <summary>
    /// Дефолтный контроллер
    /// </summary>
    [Route("api")]
    public class HomeController : Controller
    {
        /// <summary>
        /// хранилище бланков с кандидатами
        /// </summary>
        public static BlankStore BlanksStore = new BlankStore();

        /// <summary>
        /// хранилище голосов
        /// </summary>
        public static VoteStore VotesStore = new VoteStore(BlanksStore.Blanks);

        private static readonly Dictionary<string, User> RegistrationStore = new Dictionary<string, User>();

        /// <summary>
        ///  регистрация пользователя по ФИО   
        /// </summary>
        /// <returns>guid</returns>
        [Route("registration")]
        [HttpPost]
        public IActionResult Registration([FromQuery] string username)
        {
            if (username.Equals("")) return BadRequest("Введите имя");

            if (RegistrationStore.ContainsKey(username))
            {
                return BadRequest("Вы уже зарегистрированы"); 
            }
            else
            {
                string guid = RegistrationStore.Count.ToString();
                RegistrationStore.Add(username, new User { Guid = guid, Name = username});
                return Ok(guid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("blanks")]
        [HttpGet]
        public List<User> GetAllBlanks()
        {
            return BlanksStore.Blanks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("votes")]
        [HttpGet]
        public string GetAllVotes()
        {
            return JsonConvert.SerializeObject(VotesStore.GetVotes(), Formatting.Indented);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("finished")]
        [HttpGet]
        public IActionResult Finished([FromQuery] string idUser)
        {
            if (RegistrationStore.Count(i => i.Value.Guid.Equals(idUser)) == 0)
            {
                return BadRequest("Вы не зарегистрированы");
            }
            return Ok("Ваше голосование окончено");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("votes")]
        [HttpPost]
        public IActionResult CastVote([FromQuery] string id, [FromQuery] string candidate)
        {
            if (RegistrationStore.Count(i => i.Value.Guid.Equals(id)) == 0)
            {
                return BadRequest("Вы не зарегистрированы");
            }

            if (!VotesStore.AddVote(id, candidate))
            {
                return BadRequest("Вы уже голосовали");
            }

            if (BlanksStore.Blanks.Count(i => i.Guid.Equals(candidate)) == 0)
            {
                return BadRequest("Нет такого кандидата");
            }
            
            return Ok("Ваш голос учтен");
        }
    }
}
