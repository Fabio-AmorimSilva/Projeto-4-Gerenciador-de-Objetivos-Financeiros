﻿namespace FinancialGoalsManager.Application.Common.Responses;

public class UnprocessableResponse<T> : ApiResponse<T>
{
    public UnprocessableResponse(string message)
    {
        Error(message);
        StatusCode = 422;
    }
}