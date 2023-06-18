using System;

namespace CleanArchMvc.API.Models {
    public class UserToken {

        #region Propriedades
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        #endregion

    }
}
