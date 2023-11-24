FROM golang:1.18.1-alpine as builder

# Set the Current Working Directory inside the container
WORKDIR /app

# Retrieve application dependencies using go modules.
COPY go.* ./
RUN go mod download -x

# Build the binary.
# -mod=readonly ensures immutable go.mod and go.sum in container builds.
COPY controller ./controller
COPY model ./model
COPY trie ./trie
COPY worker ./worker
COPY main.go ./
COPY .env ./

RUN CGO_ENABLED=0 GOOS=linux go build -mod=readonly -v -o server

# Use the official Alpine image for a lean production container.
# https://hub.docker.com/_/alpine
# https://docs.docker.com/develop/develop-images/multistage-build/#use-multi-stage-builds
FROM alpine:3
RUN apk add --no-cache ca-certificates

# This container exposes port 8080 to the outside world
EXPOSE 8080

# Copy the binary to the production image from the builder stage.
COPY --from=builder /app/server /server
COPY --from=builder /app/.env .env

# Run the web service on container startup.
CMD ["./server"]
