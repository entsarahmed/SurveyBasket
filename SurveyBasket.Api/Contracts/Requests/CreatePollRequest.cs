using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.Contracts.Requests;

public record CreatePollRequest(
 [MinLength(3)]
 [MaxLength(5)]
  string Title,
  string Description
    );
