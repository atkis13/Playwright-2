using System.Text.Json;
using Allure.NUnit;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
namespace TestProject1;

[Parallelizable(ParallelScope.Self)]
[AllureNUnit]
public class Tests : PageTest
{
    private IAPIRequestContext request = null;
    
    [SetUp]
    public async Task Init(){

        request = await Playwright.APIRequest.NewContextAsync(new(){

            BaseURL = "https://fakestoreapi.com/",
        });

    }
        
    [Test]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {
        await Page.GotoAsync("https://playwright.dev");


        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

        // create a locator
        var getStarted = Page.Locator("text=Get Started");

        // Expect an attribute "to be strictly equal" to the value.
        await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

        // Click the get started link.
        await getStarted.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
    }


    [Test]
    public async Task PostAPITest(){        

        var data = new Dictionary<string, string>(){

            {"title", "main title"},
            {"price", "17"},
            {"description", "test"},
            {"image", "https://i.test.ac"},
            {"category", "electronics"}
        };

        var response = await request.PostAsync("/products", new() {DataObject = data});
        Assert.True(response.Ok);
              

    } 

    [Test]
    public async Task GetAPITest(){

        var response = await request.GetAsync("/products/1");
        Assert.True(response.Ok);
        var jsonBody = await response.JsonAsync();
        var content = jsonBody.Value.Deserialize<FakeApiEntities>();        
        content.Title.Should().Contain("Fjallraven");   
              

    }


}
