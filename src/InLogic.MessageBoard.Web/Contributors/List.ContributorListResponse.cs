using InLogic.MessageBoard.Web.ContributorEndpoints;

namespace InLogic.MessageBoard.Web.Endpoints.ContributorEndpoints;

public class ContributorListResponse
{
  public List<ContributorRecord> Contributors { get; set; } = new();
}
