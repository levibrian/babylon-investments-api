#custom variables 
variable "path" {}
variable "environment" {}

variable "security_group_id" {}

variable "config" {

  type = object({
    function_name         = string
    description           = string
    handler               = string
    filename              = string
    role_arn              = string
    concurrency           = number
    memory_size           = number
    environment_variables = map(string)
  })
}

#lambda module

variable "runtime" {
  description = "See Runtimes for valid values."
  default     = "dotnetcore3.1"
  type        = string
}


variable "lambda_timeout" {
  default     = 30
  description = "The amount of time your Lambda Function has to run in seconds. Defaults to 5"
  type        = number
}

variable "tags" {
  default     = {}
  description = "A mapping of tags to assign to the object."
  type        = map(any)
}

variable "vpc_config" {
  default     = null
  description = "Provide this to allow your function to access your VPC. Fields documented below. See Lambda in VPC."
  type = object({
    security_group_ids = list(string)
    subnet_ids         = list(string)
  })
}

variable "tracing_config" {
  default     = { mode = "Active" }
  description = "Use AWS X-Ray to collect data about events that your function processes, and to identify the cause of errors in your serverless applications. Can be either PassThrough or Active."
  type = object({
    mode = string
  })
}

variable "environment_config" {
  default     = null
  description = "The Lambda environment's configuration settings."
  type        = map(string)
}

variable "publish" {
  default     = true
  description = "Whether to publish creation/change as new Lambda Function Version. Defaults to true."
  type        = bool
}

variable "log_retention" {
  default     = 400
  description = "Specifies the number of days you want to retain log events in the specified log group."
  type        = number
}

variable "event_age_in_seconds" {
  default     = 100
  description = "Maximum age of a request that Lambda sends to a function for processing in seconds. Valid values between 60 and 21600."
  type        = number
}

variable "retry_attempts" {
  default     = 0
  description = "Maximum number of times to retry when the function returns an error. Valid values between 0 and 2. Defaults to 2."
  type        = number
}

variable "source_code_hash" {
  default     = null
  description = "Used to trigger updates when file contents change.  Must be set to a base64-encoded SHA256 hash of the package file specified with either filename or s3_key."
  type        = string
}

variable "layers" {
  default     = null
  description = "List of Lambda Layer Version ARNs (maximum of 5) to attach to your Lambda Function"
  type        = list(string)
}
