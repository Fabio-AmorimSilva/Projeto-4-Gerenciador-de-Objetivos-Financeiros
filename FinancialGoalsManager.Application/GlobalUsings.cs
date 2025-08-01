global using System.Reflection;
global using Ardalis.Specification.EntityFrameworkCore;
global using FinancialGoalsManager.Application.Common.Interfaces;
global using FinancialGoalsManager.Application.Common.Responses;
global using FinancialGoalsManager.Application.IntegrationEvents.Events;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.CreateFinancialGoal;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.DeleteFinancialGoal;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.GetFinancialGoal;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.ListFinancialGoals;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.SimulateFinancialGoalProgress;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.UpdateFinancialGoal;
global using FinancialGoalsManager.Application.UseCases.Transactions.CreateTransaction;
global using FinancialGoalsManager.Application.UseCases.Transactions.DeleteTransaction;
global using FinancialGoalsManager.Application.UseCases.Transactions.GetTransaction;
global using FinancialGoalsManager.Application.UseCases.Transactions.ListTransactions;
global using FinancialGoalsManager.Application.UseCases.Users.CreateUser;
global using FinancialGoalsManager.Application.UseCases.Users.DeleteUser;
global using FinancialGoalsManager.Application.UseCases.Users.Login;
global using FinancialGoalsManager.Application.UseCases.Users.UpdatePassword;
global using FinancialGoalsManager.Application.UseCases.Users.UpdateUser;
global using FinancialGoalsManager.Domain.Common.Interfaces;
global using FinancialGoalsManager.Domain.Entities;
global using FinancialGoalsManager.Domain.Enums;
global using FinancialGoalsManager.Domain.EventBus;
global using FinancialGoalsManager.Domain.Exceptions;
global using FinancialGoalsManager.Domain.Messages;
global using FinancialGoalsManager.Domain.Models;
global using FinancialGoalsManager.Domain.Specifications;
global using FluentValidation;
global using Microsoft.EntityFrameworkCore;
global using FinancialGoalsManager.Application.IntegrationEvents.EventHandling;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalProgress;
global using FinancialGoalsManager.Application.UseCases.FinancialGoals.TrackFinancialGoalsReport;
global using FinancialGoalsManager.Domain.Notifications.Mail;