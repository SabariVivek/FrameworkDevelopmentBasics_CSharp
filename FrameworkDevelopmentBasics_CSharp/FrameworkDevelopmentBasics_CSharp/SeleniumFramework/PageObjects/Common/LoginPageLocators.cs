﻿using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements;
using FrameworkDevelopmentBasics_CSharp.SeleniumFramework.Utils.WebElements.Base;

namespace FrameworkDevelopmentBasics_CSharp.SeleniumFramework.PageObjects.Common
{
    public class LoginPageLocators
    {
        public Input Username => new Input(Locators.ID, "username");

        public Input Password => new Input(Locators.ID, "password");

        public Button Login => new Button(Locators.XPATH, "//*[contains(text(),'Login')]/parent::button");

        public Button Logout => new Button(Locators.XPATH, "//*[contains(text(),'Logout')]");
    }
}