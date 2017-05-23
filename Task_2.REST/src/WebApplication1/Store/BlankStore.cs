using System.Collections.Generic;
using WebApp.WebUI.Models;

namespace WebApp.WebUI.Store
{
    public class BlankStore
    {
        /// <summary>
        /// 
        /// </summary>
        public List<User> Blanks { get; } = new List<User>
        {
            new User {Guid = "0", Name = "Василий Пупкин"},
            new User {Guid = "1", Name = "Игорь Павлов"},
            new User {Guid = "2", Name = "Лариса Феофанова"},
            new User {Guid = "3", Name = "Лидия Загнойко"},
            new User {Guid = "4", Name = "Гавриил Мондатов"}
        };
    }
}
