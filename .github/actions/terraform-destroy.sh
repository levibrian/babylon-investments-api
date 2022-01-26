set -e 
BASE_FOLDER=$(pwd)

log_action() {
    echo "${1^^} ..."
}

log_key_value_pair() {
    echo "    $1: $2"
}

set_up_aws_user_credentials() {
    unset AWS_SESSION_TOKEN
    export AWS_DEFAULT_REGION=$1
    export AWS_ACCESS_KEY_ID=$2
    export AWS_SECRET_ACCESS_KEY=$3
}

log_action "planning terraform"

log_key_value_pair "working-folder" $BASE_FOLDER

REGION="$1"
log_key_value_pair "region" "$REGION"

ACCESS_KEY="$2"
log_key_value_pair "access-key" "$ACCESS_KEY"

SECRET_KEY="$3"

TFM_FOLDER=$4
log_key_value_pair "terraform-folder" $TFM_FOLDER

ENVIRONMENT=$5
log_key_value_pair "environment" $ENVIRONMENT

STACK_NAME=$6
log_key_value_pair "stack-name" $STACK_NAME

TFVARS_FILE=$7
log_key_value_pair "terraform-var-file" $TFVARS_FILE

set_up_aws_user_credentials $REGION $ACCESS_KEY $SECRET_KEY

WORKING_FOLDER="$BASE_FOLDER/$TFM_FOLDER"

cd $WORKING_FOLDER

terraform workspace select $ENVIRONMENT || terraform workspace new $ENVIRONMENT

terraform init -backend-config="access_key=$ACCESS_KEY" -backend-config="secret_key=$SECRET_KEY"

if [ "$STACK_NAME" = "" ]; then 
    terraform workspace select $ENVIRONMENT || terraform workspace new $ENVIRONMENT
    
    terraform init -backend-config="access_key=$ACCESS_KEY" -backend-config="secret_key=$SECRET_KEY"
    
    terraform destroy -auto-approve -var-file=$TFVARS_FILE -var env_suffix=$STACK_NAME
else
    terraform workspace select $STACK_NAME || terraform workspace new $STACK_NAME

    terraform init -backend-config="access_key=$ACCESS_KEY" -backend-config="secret_key=$SECRET_KEY"

    terraform destroy -auto-approve -var-file=$TFVARS_FILE -var env_suffix=$STACK_NAME
fi

cd $BASE_FOLDER