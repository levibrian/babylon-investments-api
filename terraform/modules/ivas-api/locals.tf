locals {
  region = data.aws_region.current.name

  access_log_settings = {
    requestId = "$context.requestId"
    identity = {
      ip     = "$context.identity.sourceIp"
      caller = "$context.identity.caller"
      user   = "$context.identity.user"
    }
    requestTime      = "$context.requestTime"
    requestTimeEpoch = "$context.requestTimeEpoch"
    httpMethod       = "$context.httpMethod"
    resourcePath     = "$context.resourcePath"
    status           = "$context.status"
    protocol         = "$context.protocol"
    responseLength   = "$context.responseLength"
    xrayTraceId      = "$context.xrayTraceId"
  }
}