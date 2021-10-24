data "aws_ssm_parameter" "subnet_id_1" { name = "/common/vpc/privsubnet_id1" }
data "aws_ssm_parameter" "subnet_id_2" { name = "/common/vpc/privsubnet_id2" }