﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Share.BaseCore.Behaviors
{
    // xử lí validator trước khi đến điểm cuối, tức là xử lí trong đường ống trước khi đến điểm cuối MediatR để xử lí
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // var typeName = request.GetGenericTypeName();

            Log.Information("----- Validating command ");

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                Log.Warning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", "", request, failures);
                string error = "";
                foreach (var item in failures)
                {
                    error = error + " " + item.PropertyName + " " + item.ErrorMessage;
                }
                throw new BaseException(
                    $"Command Validation Errors for type (Models Validator) {typeof(TRequest).Name} .Message:{error} ", new ValidationException("Validation exception", failures));
            }

            return await next();
        }
    }
}