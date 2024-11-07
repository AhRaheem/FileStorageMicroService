
global using StorageService.Application.Interfaces;
global using StorageService.Domain.Repositories;
global using StorageService.Infrastructure.Repositories;
global using MediatR;
global using StorageService.Application.Commands.AddFile;
global using StorageService.Infrastructure.Persistence;
global using StorageService.Application.Queries.RetrieveFile;
global using Microsoft.AspNetCore.Mvc;
global using Grpc.Core;
global using StorageService.Application.Commands.DeleteFile;
global using StorageService.Protos;
global using StorageService.Infrastructure.Messaging;
