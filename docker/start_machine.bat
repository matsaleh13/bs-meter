REM Start up the docker machine

SET MACHINE=bs-meter-dev

REM Start the machine
docker-machine start %MACHINE%

REM Get the environment variables and store in a batch file
docker-machine env --shell cmd %MACHINE% | grep -v # > %MACHINE%.bat

REM Set the env variables in the env.
CALL %MACHINE%.bat

REM Get the IP address and store it in an environment file
REM NOTE: This is a really ugly hack (IMO)
TEMPFILE=%MACHINE%-ip.tmp
docker-machine ip %MACHINE% > %TEMPFILE%
SET /p REDIS_HOST=<%TEMPFILE%
DEL %TEMPFILE%

ECHO REDIS_HOST=%REDIS_HOST% > corpus.env


