import requests
import json
import os
import shutil # for copying files
import datetime
from timeit import default_timer as timer

def remove_timestamps(obj):
    newobj = dict.copy(obj)
    newobj["created_at"] = ""
    newobj["updated_at"] = ""
    return newobj



address = "http://localhost:3000/api/v1"
def test_get():
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    os.makedirs("testing/results/GET/" + test_datetime)
    diagnostics = {}
    files = os.listdir("testing/test_data")
    for file in files:
        start = timer()
        response = requests.get(address + "/" + file.split(".")[0], #remove .json from filename
                                headers=
                                {'API_KEY': 'a1b2c3d4e5'}
                                )
        
        response_time = timer() - start
        with open("testing/test_data/" + file) as json_data:
            response_data = response.json()
            db_data = json.load(json_data)
            # print(response_data) run "python run_tests.py -s" in terminal to enable print
            success = response_data == db_data
            results_file_name = "GET_" + file.split(".")[0]# + "_" + test_datetime
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/GET/{test_datetime}/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)

            assert success


def test_post():
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M")
    os.makedirs("testing/results/POST/" + test_datetime)
    diagnostics = {}
    post_objects = {}
    files = os.listdir("testing/test_data")
    for file in files: #e.g. inventories.json 
        post_objects[file.split(".")[0]] = {} #set up dictionary for objects
    for obj in post_objects: #populate dictionary with test objects
        match obj:
            case "clients":#todo
                obj = {"id": 999, "name": "Test_Client", "address": "Nowhere", "city": "Nowhere", "zip_code": "00000", "province": "Nowhere", "country": "Nowhere", "contact_name": "Test_Client", "contact_phone": "242.732.3483x2573", "contact_email": "test_client@example.net", "created_at": "", "updated_at": ""}
            case "inventories":#todo
                obj = {"id": 999, "item_id": "P000001", "description": "Face-to-face clear-thinking complexity", "item_reference": "sjQ23408K", "locations": [3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817], "total_on_hand": 262, "total_expected": 0, "total_ordered": 80, "total_allocated": 41, "total_available": 141, "created_at": "2015-02-19 16:08:24", "updated_at": "2015-09-26 06:37:56"}
            case "item_groups":#todo
                obj = {"id": 999, "name": "Electronics", "description": "", "created_at": "1998-05-15 19:52:53", "updated_at": "2000-11-20 08:37:56"}
            case "item_lines":#todo
                obj = {"id": 999, "name": "Tech Gadgets", "description": "", "created_at": "2022-08-18 07:05:25", "updated_at": "2023-05-15 15:44:28"}
            case "item_types":#todo
                obj = {"id": 999, "name": "Laptop", "description": "", "created_at": "2001-11-02 23:02:40", "updated_at": "2008-07-01 04:09:17"}
            case "items":#todo
                obj = {
                    "uid": "999",
                    "code": "sjQ23408K",
                    "description": "Face-to-face clear-thinking complexity",
                    "short_description": "must",
                    "upc_code": "6523540947122",
                    "model_number": "63-OFFTq0T",
                    "commodity_code": "oTo304",
                    "item_line": 11,
                    "item_group": 73,
                    "item_type": 14,
                    "unit_purchase_quantity": 47,
                    "unit_order_quantity": 13,
                    "pack_order_quantity": 11,
                    "supplier_id": 34,
                    "supplier_code": "SUP423",
                    "supplier_part_number": "E-86805-uTM",
                    "created_at": "2015-02-19 16:08:24",
                    "updated_at": "2015-09-26 06:37:56"
                }
            case "locations":#todo
                obj = {"id": 999, "warehouse_id": 1, "code": "A.1.0", "name": "Row: A, Rack: 1, Shelf: 0", "created_at": "1992-05-15 03:21:32", "updated_at": "1992-05-15 03:21:32"}
            case "orders":#todo
                obj = {
                    "id": 999,
                    "source_id": 33,
                    "order_date": "2019-04-03T11:33:15Z",
                    "request_date": "2019-04-07T11:33:15Z",
                    "reference": "ORD00001",
                    "reference_extra": "Bedreven arm straffen bureau.",
                    "order_status": "Delivered",
                    "notes": "Voedsel vijf vork heel.",
                    "shipping_notes": "Buurman betalen plaats bewolkt.",
                    "picking_notes": "Ademen fijn volgorde scherp aardappel op leren.",
                    "warehouse_id": 18,
                    "ship_to": None, #supposed to be null
                    "bill_to": None, #supposed to be null
                    "shipment_id": 1,
                    "total_amount": 9905.13,
                    "total_discount": 150.77,
                    "total_tax": 372.72,
                    "total_surcharge": 77.6,
                    "created_at": "2019-04-03T11:33:15Z",
                    "updated_at": "2019-04-05T07:33:15Z",
                    "items": [] #todo
                }
            case "shipments":#todo
                obj =  {
                    "id": 999,
                    "order_id": 1,
                    "source_id": 33,
                    "order_date": "2000-03-09",
                    "request_date": "2000-03-11",
                    "shipment_date": "2000-03-13",
                    "shipment_type": "I",
                    "shipment_status": "Pending",
                    "notes": "Zee vertrouwen klas rots heet lachen oneven begrijpen.",
                    "carrier_code": "DPD",
                    "carrier_description": "Dynamic Parcel Distribution",
                    "service_code": "Fastest",
                    "payment_type": "Manual",
                    "transfer_mode": "Ground",
                    "total_package_count": 31,
                    "total_package_weight": 594.42,
                    "created_at": "2000-03-10T11:11:14Z",
                    "updated_at": "2000-03-11T13:11:14Z",
                    "items": [] #todo
                }
            case "suppliers":#todo
                obj = {"id": 999, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "1971-10-20 18:06:17", "updated_at": "1985-06-08 00:13:46"}
            case "transfers":#todo
                obj = {
                    "id": 999,
                    "reference": "TR00001",
                    "transfer_from": None, #supposed to be null
                    "transfer_to": 9229,
                    "transfer_status": "Completed",
                    "created_at": "2000-03-11T13:11:14Z",
                    "updated_at": "2000-03-12T16:11:14Z",
                    "items": [
                        {
                            "item_id": "P007435",
                            "amount": 23
                        }
                    ]
                }
            case "warehouses":#todo
                obj = {"id": 999, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AS", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "1983-04-13 04:59:55", "updated_at": "2007-02-08 20:11:00"}

    for file in files: #now add the items and test
        start = timer()
        obj_to_post = post_objects[file.split(".")[0]]
        shutil.copyfile("data/" + file, "testing/test_data_backup/" + file) #backup original file
        response = requests.post(address + "/" + file.split(".")[0], #remove .json from filename
                                json=obj_to_post,
                                headers=
                                {'API_KEY': 'a1b2c3d4e5',
                                'content-type': 'application/json'}
                                )
        response_time = timer() - start
        with open("data/" + file) as json_data:
            #response_data = response.json()
            db_data = json.load(json_data)
            success = db_data.__contains__(remove_timestamps(obj_to_post)) 
            #likely fails because POST updated the created_at and updated_at parameters
            #AND/OR __contains__ compares identity which would return false even if parameters are the same

            # print(response_data) run "python run_tests.py -s" in terminal to enable print
            # success = response_data == db_data
            results_file_name = "POST_" + file.split(".")[0]# + "_" + test_datetime
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/POST/{test_datetime}/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)

            shutil.copyfile("testing/test_data_backup/" + file, "data/" + file) #restore file
            assert response.ok
            assert response.status_code == 201
            assert success
            
    