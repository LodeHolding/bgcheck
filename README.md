# bgcheck
A tool to check basic functionality of [BlueGiga BLED112 Dongle](https://www.silabs.com/products/wireless/bluetooth/bluetooth-low-energy-modules/bled112-bluetooth-smart-dongle).

## Usage

# Dongle installation

1. Insert the BlueGiga BLED112 Dongle into an USB port.
2. Make sure the drivers are installed and the dongle has created a COM port. Check it in the [Windows Device Manager](https://support.microsoft.com/en-us/help/4026149/windows-open-device-manager). You should see `Ports (COM & LPT)` --> `Bluegiga Bluetooth Low Energy (COM3)`. The `COM3` is the serial port created by the dongle, you may have different port number.
3. If you do not see the COM port in the Device Manager, try install the drivers. You should have drivers with the app you have for the Dongle. Or try [Rouvy drivers](https://cdn.virtualtraining.eu/downloads/setup_rouvy_drivers.exe).

# bgcheck

1. Select the port where is your dongle installed. Click *Open port*.
2. You should see a text like:
    ```c#
    Port opened: COM3
    hello: ok
    get_info: version: 1.3.2.122, ll_version: 3, protocol_version: 1, hw: 3
    ```
    Then your dongle is OK. If you see some errors or do not see the `get_info` in the output, then your dongle has another COM port (try another one), or is broken.
3. Try BLE scan by the buttons in the bottom right corner. You must have some BLE devices broadcasting BLE advertisements. If you do not see any advertisements in the `blecheck` output, then first check you have some BLE device nearby. If you are sure you have some, then your dongle may be misconfigured or broken. 

Note: if you see errors like:
* `port closed!` - click *Open port* again.
* `ble_scan_end: error 385` - you have already clicked the Start or Stop Scanning.

## Thanks to
* [@jrowberg](https://github.com/jrowberg) for the [BGLib](https://github.com/jrowberg/bglib). This project is based on the `BLEScanner` example from the BGLib project.
* [Rouvy](https://rouvy.com) for sponsoring work on this tool.
