using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task NavigatToNimbleSite()
    {
        await Page.GotoAsync("https://demo.realworld.io/");

        Random rnd = new Random();
        int num = rnd.Next();

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Home"));

        // create a locator
        var userName = "JohnSmith" + num;
        var email = "John.Smith" + num + "@gmail.com";
        var passWord = "genericPassword1234!";
        var signUp = Page.Locator("text=Sign up");
        var userNameInput = Page.GetByPlaceholder("Username");
        var emailInput = Page.GetByPlaceholder("Email");
        var passWordInput = Page.GetByPlaceholder("Password");

        var signUpSubmitButton = Page.Locator("button", new() { HasTextString = "Sign up" });

        // Navigate to Sign Up Page
        await signUp.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex("https://demo.realworld.io/#/register"));

        await userNameInput.FillAsync(userName);
        await emailInput.FillAsync(email);
        await passWordInput.FillAsync(passWord);

        await signUpSubmitButton.ClickAsync();

        //Verification Locator
        var loggedInUsername = Page.GetByText(userName);
        var settings = Page.GetByText("settings");
        //var settings2 = Page.Locator("nav-link", new() { HasTextString = "settings" });
        var editorHREF = Page.Locator("a", new() { HasTextString = "editor" });
        var editor = Page.GetByText("New Article");
        var signIn = Page.GetByText("Sign in");


        await Expect(settings).ToBeVisibleAsync();
        await Expect(loggedInUsername).ToBeVisibleAsync();

        await loggedInUsername.ClickAsync();

        await editor.ClickAsync();
    }
    [Test]
    public async Task LoginAndVerifySuccess()
    {
        await Page.GotoAsync("https://demo.realworld.io/");

        await Expect(Page).ToHaveTitleAsync(new Regex("Home"));

        //locators
        var userName = "JohnSmith92";
        var email = "John.Smith92@gmail.com";
        var passWord = "genericPassword1234!";
        var signUp = Page.Locator("text=Sign up");
        var userNameInput = Page.GetByPlaceholder("Username");
        var emailInput = Page.GetByPlaceholder("Email");
        var passWordInput = Page.GetByPlaceholder("Password");
        var signInButton = Page.Locator("button", new() { HasTextString = "Sign in" });

        //Verification Locator
        var loggedInUsername = Page.GetByText(userName);
        var settings = Page.GetByText("settings");
        //var settings2 = Page.Locator("nav-link", new() { HasTextString = "settings" });
        var editorHREF = Page.Locator("a", new() { HasTextString = "editor" });
        var editor = Page.GetByText("New Article");
        var signIn = Page.GetByText("Sign in");

        await signIn.ClickAsync();

        await emailInput.FillAsync(email);
        await passWordInput.FillAsync(passWord);

        await signInButton.ClickAsync();

        await Expect(settings).ToBeVisibleAsync();
        await Expect(loggedInUsername).ToBeVisibleAsync();

        await loggedInUsername.ClickAsync();

        await editor.ClickAsync();

        //await editorHREF.ClickAsync();


    }
}