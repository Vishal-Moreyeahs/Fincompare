#!/bin/bash
echo "Restarting service..."
systemctl restart fincompare.service
if [ $? -eq 0 ]; then
    echo "Service restarted successfully."
else
    echo "Failed to restart service."
    exit 1
fi
