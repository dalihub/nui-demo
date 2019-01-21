#!/bin/bash

SCRIPT_FILE=$(readlink -f $0)
SCRIPT_DIR=$(dirname $SCRIPT_FILE)

EXAMPLES=$(ls $SCRIPT_DIR)

BUILD_COMMAND="build -warnaserror"
CLEAN_COMMAND="clean"

ERROR=0

Run()
{
  DOTNET_COMMAND=$1

  for example in $EXAMPLES
  do
    if [[ -d $example ]]
    then
      cd $example
      echo "********************************************************"
      echo "Building $example"
      echo "********************************************************"
      command="dotnet $DOTNET_COMMAND"
      echo $command
      eval $command || ERROR=$?
      cd ..
    fi
  done
}

Usage() {
  echo "Usage: $0 [command]"
  echo "Commands:"
  echo "    full       Build all examples (default if no option)"
  echo "    clean      Clean all examples"
}

if [[ $1 = "" ]]
then
  Run $BUILD_COMMAND
else
  CMD=$1; shift;
  case "$CMD" in
    full |--full |-f) Run $BUILD_COMMAND ;;
    clean|--clean|-c) Run $CLEAN_COMMAND ;;
    *) Usage ;;
  esac
fi

exit $ERROR
