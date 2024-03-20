Firstly, you need to dockerize rabbit mq with this code
docker run --platform linux/arm64 -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
