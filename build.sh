#!/bin/bash

SCRIPT_FILE=$(readlink -f $0)
SCRIPT_DIR=$(dirname $SCRIPT_FILE)

EXAMPLES=$(ls $SCRIPT_DIR)

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

FullBuild()
{
  Run build
}

if [[ $1 = "" ]]
then
  Run build
else
  CMD=$1; shift;
  case "$CMD" in
    full |--full |-f) Run build ;;
    clean|--clean|-c) Run clean ;;
    *) Usage ;;
  esac
fi

exit $ERROR
