using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InLogic.MessageBoard.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;
