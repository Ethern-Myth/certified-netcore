using backend.Data;
using Microsoft.Extensions.Options;
using Octokit;

namespace backend.Helper;

public class UploadHelper
{
    private readonly GitHubClient gitHubClient;
    private readonly GithubContext githubContext;
    public UploadHelper(IOptions<GithubContext> githubContext)
    {
        gitHubClient = new GitHubClient(new ProductHeaderValue("certified-netcore"));
        this.githubContext = githubContext.Value;
    }
    public async Task<int> GetInfo()
    {
        var gitHubClient = new GitHubClient(new ProductHeaderValue("certified-netcore"));
        var user = await gitHubClient.User.Get("ethern-myth");
        return user.PublicRepos;
    }
    public async Task UploadFile()
    {
        gitHubClient.Credentials = new Credentials(githubContext.GitHubToken);
    }
}
