using Burgerama_Burger_Shop_App.src.handlers;
using Burgerama_Burger_Shop_App.src.validators;
using Burgerama_Burger_Shop_App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerama_Burger_Shop_Test.src.handler_tests
{
    public class LoginHandlerTests
    {
        [Theory]
        [InlineData("Fabian.Noll@gmail.com")]
        [InlineData("storz@ks.de")]
        [InlineData("admin")]
        [InlineData("manager")]
        public void SetEmailPass(string email)
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            bool output;

            output = loginHandler.SetEmail(email);

            Assert.True(output);
        }

        [Theory]
        [InlineData("Fabian@")]
        [InlineData("storz")]
        [InlineData("")]
        [InlineData("  ")]
        public void SetEmailFail(string email)
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            bool output;

            output = loginHandler.SetEmail(email);

            Assert.False(output);
        }

        [Fact]
        public void CheckForManagerPass()
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            loginHandler.email = "manager";
            loginHandler.password = "39F968F400E6B06A5153F37683C348C94C948539B17636C0529A4E833ACE9C40";
            bool output;

            output = loginHandler.IsUserManager();

            Assert.True(output);
        }

        [Theory]
        [InlineData("manager","")]
        [InlineData("manager", "king")]
        [InlineData("", "39F968F400E6B06A5153F37683C348C94C948539B17636C0529A4E833ACE9C40")]
        public void CheckForManagerFail(string email, string password)
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            loginHandler.email = email;
            loginHandler.password = password;
            bool output;

            output = loginHandler.IsUserManager();

            Assert.False(output);
        }

        [Theory]
        [InlineData("fabian.noll@gmail.com", "65A8CDC868F6772F6DBB72932FD008F53CF8F43090D37BDBC1B39B4663DE2791")]
        [InlineData("florian.armbruster@gmx.de", "69CA6781A5A3FD9E6C06381EFCA699F13E947678B04E7EC976B5D52290AB8F33")]
        [InlineData("heinrich.summer@gmx.de", "22DFDA2D0DA77E6C5BD7F87F05E7ACC1D1617A1FE59DEE0A17A61A034359D786")]
        [InlineData("dullenkopf.christian@siemens.de", "22DFDA2D0DA77E6C5BD7F87F05E7ACC1D1617A1FE59DEE0A17A61A034359D786")]
        [InlineData("simon.brubar@t-online.de", "56E1DFDF22C508710E4DE86628E0412B0F4E8D28D3A037B604ABC2D663A8DCE4")]
        public void UserRegisteredPass(string email, string password)
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            loginHandler.LoadUserData();
            loginHandler.email = email;
            loginHandler.password = password;
            bool output;

            output = loginHandler.IsUserRegistered();

            Assert.True(output);
        }

        [Theory]
        [InlineData("fabian.noll@gmail.com", "22DFDA2D0DA77E6C5BD7F87F05E7ACC1D1617A1FE59DEE0A17A61A034359D786")]
        [InlineData("florian.armbruster@gmx.de", "65A8CDC868F6772F6DBB72932FD008F53CF8F43090D37BDBC1B39B4663DE2791")]
        [InlineData("22DFDA2D0DA77E6C5BD7F87F05E7ACC1D1617A1FE59DEE0A17A61A034359D786", "dullenkopf.christian@siemens.de")]
        public void UserRegisteredFail(string email, string password)
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            loginHandler.LoadUserData();
            loginHandler.email = email;
            loginHandler.password = password;
            bool output;

            output = loginHandler.IsUserRegistered();

            Assert.False(output);
        }

        [Fact]
        public void ReturnUserAfterLogin()
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            loginHandler.LoadUserData();
            User user = new User();
            loginHandler.email = "fabian.noll@gmail.com";
            loginHandler.password = "65A8CDC868F6772F6DBB72932FD008F53CF8F43090D37BDBC1B39B4663DE2791";

            user = loginHandler.ReturnUser();

            Assert.NotNull(user);
            Assert.Equal(loginHandler.email, user.email);
            Assert.Equal(loginHandler.password, user.password);
            Assert.Equal("Tuttlingen", user.city);

        }

        [Fact]
        public void ReturnUserAfterLoginNull()
        {
            LoginHandler loginHandler = new LoginHandler("src/test data/", "user_data_test.xml");
            loginHandler.LoadUserData();
            User user = new User();
            loginHandler.email = "";
            loginHandler.password = "";

            user = loginHandler.ReturnUser();

            Assert.Null(user);
        }
    }
}
