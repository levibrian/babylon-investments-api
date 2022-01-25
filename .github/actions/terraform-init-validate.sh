set -e 
WORKING_FOLDER=$(pwd)

log_action() {
    echo "${1^^} ..."
}

log_key_value_pair() {
    echo "    $1: $2"
}

log_action "validating terraform"
TFM_PATH=$1
log_key_value_pair "tfm-path" $TFM_PATH
ACCESS_KEY=$2
log_key_value_pair "access-key" $ACCESS_KEY
SECRET_KEY=$3
BACKEND=$4
log_key_value_pair "use-backend" $BACKEND

cd $WORKING_FOLDER/$TFM_PATH

terraform init -backend=$BACKEND -backend-config="access_key=$ACCESS_KEY" -backend-config="secret_key=$SECRET_KEY"
terraform validate -no-color

cd $WORKING_FOLDER