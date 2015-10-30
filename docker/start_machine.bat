REM Start up the docker machine

SET MACHINE=bs-meter-dev

REM Start the machine
docker-machine start %MACHINE%

REM Get the environment variables and store in a batch file
docker-machine env --shell cmd %MACHINE% | grep -v # > %MACHINE%.bat

REM Set the env variables in the env.
CALL %MACHINE%.bat

REM Get the IP address and store it in a file
docker-machine ip %MACHINE% > %MACHINE%-address.txt
