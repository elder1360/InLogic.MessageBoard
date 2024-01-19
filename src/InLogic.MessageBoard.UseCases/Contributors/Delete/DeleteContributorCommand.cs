using Ardalis.Result;
using Ardalis.SharedKernel;

namespace InLogic.MessageBoard.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;
