using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Grpc.Core.Interceptors.Interceptor;
using Serilog;
using System.Diagnostics;
using Share.Base.Core.Extensions;

namespace Share.Base.Core.Grpc
{
    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GrpcExceptionInterceptor(IHttpContextAccessor httpContextAccesso)
        {
            _httpContextAccessor = httpContextAccesso;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();
                var metadata = new Metadata();
                string accessToken = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"] ?? "";
                if (!string.IsNullOrWhiteSpace(accessToken) && accessToken.StartsWith("Bearer"))
                {
                    metadata.Add("Authorization", accessToken);
                }
                var callOption = context.Options.WithHeaders(metadata);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, callOption);
                stopwatch.Stop();
                LogExtension.Information("Call grpc: " + stopwatch.ElapsedMilliseconds);
                return base.AsyncUnaryCall(request, context, continuation);
            }
            catch (RpcException e)
            {
                Log.Error("Error calling via grpc: {Status} - {Message}", e.Status, e.Message);
                throw e;
            }

        }


    }

    public class ServerLoggerInterceptor : Interceptor
    {
        private readonly ILogger<ServerLoggerInterceptor> _logger;

        public ServerLoggerInterceptor(ILogger<ServerLoggerInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogCall<TRequest, TResponse>(MethodType.Unary, context);

            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                // Note: The gRPC framework also logs exceptions thrown by handlers to .NET Core logging.
                _logger.LogError(ex, $"Error thrown by {context.Method}.");

                throw;
            }
        }

        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            LogCall<TRequest, TResponse>(MethodType.ClientStreaming, context);
            return base.ClientStreamingServerHandler(requestStream, context, continuation);
        }

        public override Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            LogCall<TRequest, TResponse>(MethodType.ServerStreaming, context);
            return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }

        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            LogCall<TRequest, TResponse>(MethodType.DuplexStreaming, context);
            return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
        }

        private void LogCall<TRequest, TResponse>(MethodType methodType, ServerCallContext context)
            where TRequest : class
            where TResponse : class
        {
            LogExtension.Information($"Starting call. Type: {methodType}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
            WriteMetadata(context.RequestHeaders, "caller-user");
            WriteMetadata(context.RequestHeaders, "caller-machine");
            WriteMetadata(context.RequestHeaders, "caller-os");

            void WriteMetadata(Metadata headers, string key)
            {
                var headerValue = headers.GetValue(key) ?? "(unknown)";
                LogExtension.Information($"{key}: {headerValue}");
            }
        }
    }
}