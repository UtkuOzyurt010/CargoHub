import requests
import json
import os
import shutil # for copying files
import datetime
import string
from timeit import default_timer as timer

import pdb

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
    files = os.listdir("data")
    for file in files:
        start = timer()
        response = requests.get(address + "/" + file.split(".")[0], #remove .json from filename
                                headers=
                                {'API_KEY': 'a1b2c3d4e5'}
                                )
        
        response_time = timer() - start
        with open("data/" + file) as json_data:
            response_data = response.json()
            db_data = json.load(json_data)
            # print(response_data) run "python run_tests.py -s" in terminal to enable print
            success = response_data == db_data
            results_file_name = "GET_" + file.split(".")[0]# + "_" + test_datetime
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/GET/{test_datetime}/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)

            assert success


#make it so that this can be called for each endpoint, so the tests are ran seperately,
# rather than in a for loop
# def test_post():
#     test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
#     os.makedirs("testing/results/POST/" + test_datetime)
#     diagnostics = {}
#     post_objects = {}
#     files = os.listdir("data")
#     for file in files: #e.g. inventories.json 
#         post_objects[file.split(".")[0]] = {} #set up dictionary for objects
#     for obj in post_objects: #populate dictionary with test objects
#         match obj:
#             case "clients":#todo
#                 post_objects[obj] = {"id": 999, "name": "Test_Client", "address": "Nowhere", "city": "Nowhere", "zip_code": "00000", "province": "Nowhere", "country": "Nowhere", "contact_name": "Test_Client", "contact_phone": "242.732.3483x2573", "contact_email": "test_client@example.net", "created_at": "", "updated_at": ""}
#             case "inventories":#todo
#                 post_objects[obj] = {"id": 999, "item_id": "P000001", "description": "Face-to-face clear-thinking complexity", "item_reference": "sjQ23408K", "locations": [3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817], "total_on_hand": 262, "total_expected": 0, "total_ordered": 80, "total_allocated": 41, "total_available": 141, "created_at": "", "updated_at": ""}
#             case "item_groups":#todo
#                 post_objects[obj] = {"id": 999, "name": "Electronics", "description": "", "created_at": "", "updated_at": ""}
#             case "item_lines":#todo
#                 post_objects[obj] = {"id": 999, "name": "Tech Gadgets", "description": "", "created_at": "", "updated_at": ""}
#             case "item_types":#todo
#                 post_objects[obj] = {"id": 999, "name": "Laptop", "description": "", "created_at": "", "updated_at": ""}
#             case "items":#todo
#                 post_objects[obj] = {
#                     "uid": "999",
#                     "code": "sjQ23408K",
#                     "description": "Face-to-face clear-thinking complexity",
#                     "short_description": "must",
#                     "upc_code": "6523540947122",
#                     "model_number": "63-OFFTq0T",
#                     "commodity_code": "oTo304",
#                     "item_line": 11,
#                     "item_group": 73,
#                     "item_type": 14,
#                     "unit_purchase_quantity": 47,
#                     "unit_order_quantity": 13,
#                     "pack_order_quantity": 11,
#                     "supplier_id": 34,
#                     "supplier_code": "SUP423",
#                     "supplier_part_number": "E-86805-uTM",
#                     "created_at": "",
#                     "updated_at": ""
#                 }
#             case "locations":#todo
#                 post_objects[obj] = {"id": 999, "warehouse_id": 1, "code": "A.1.0", "name": "Row: A, Rack: 1, Shelf: 0", "created_at": "", "updated_at": ""}
#             case "orders":#todo
#                 post_objects[obj] = {
#                     "id": 999,
#                     "source_id": 33,
#                     "order_date": "2019-04-03T11:33:15Z",
#                     "request_date": "2019-04-07T11:33:15Z",
#                     "reference": "ORD00001",
#                     "reference_extra": "Bedreven arm straffen bureau.",
#                     "order_status": "Delivered",
#                     "notes": "Voedsel vijf vork heel.",
#                     "shipping_notes": "Buurman betalen plaats bewolkt.",
#                     "picking_notes": "Ademen fijn volgorde scherp aardappel op leren.",
#                     "warehouse_id": 18,
#                     "ship_to": None, #supposed to be null
#                     "bill_to": None, #supposed to be null
#                     "shipment_id": 1,
#                     "total_amount": 9905.13,
#                     "total_discount": 150.77,
#                     "total_tax": 372.72,
#                     "total_surcharge": 77.6,
#                     "created_at": "",
#                     "updated_at": "",
#                     "items": [] #todo
#                 }
#             case "shipments":#todo
#                 post_objects[obj] =  {
#                     "id": 999,
#                     "order_id": 1,
#                     "source_id": 33,
#                     "order_date": "2000-03-09",
#                     "request_date": "2000-03-11",
#                     "shipment_date": "2000-03-13",
#                     "shipment_type": "I",
#                     "shipment_status": "Pending",
#                     "notes": "Zee vertrouwen klas rots heet lachen oneven begrijpen.",
#                     "carrier_code": "DPD",
#                     "carrier_description": "Dynamic Parcel Distribution",
#                     "service_code": "Fastest",
#                     "payment_type": "Manual",
#                     "transfer_mode": "Ground",
#                     "total_package_count": 31,
#                     "total_package_weight": 594.42,
#                     "created_at": "",
#                     "updated_at": "",
#                     "items": [] #todo
#                 }
#             case "suppliers":#todo
#                 post_objects[obj] = {"id": 999, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "", "updated_at": ""}
#             case "transfers":#todo
#                 post_objects[obj] = {
#                     "id": 999,
#                     "reference": "TR00001",
#                     "transfer_from": None, #supposed to be null
#                     "transfer_to": 9229,
#                     "transfer_status": "Completed",
#                     "created_at": "",
#                     "updated_at": "",
#                     "items": [
#                         {
#                             "item_id": "P007435",
#                             "amount": 23
#                         }
#                     ]
#                 }
#             case "warehouses":#todo
#                 post_objects[obj] = {"id": 999, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AS", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "", "updated_at": ""}

#     for file in files: #now add the items and test
#         #pdb.set_trace()
#         #print("file", file)
#         #print("obj name", file.split(".")[0])
#         obj_to_post = post_objects[file.split(".")[0]]
#         #print("object_to_post", obj_to_post)
#         start = timer()
#         shutil.copyfile("data/" + file, "testing/test_data_backup/" + file) #backup original file
#         response = requests.post(address + "/" + file.split(".")[0], #remove .json from filename
#                                 json=obj_to_post,
#                                 headers=
#                                 {'API_KEY': 'a1b2c3d4e5',
#                                 'content-type': 'application/json'}
#                                 )
#         response_time = timer() - start
#         with open("data/" + file) as json_data:
#             #response_data = response.json()
#             db_data = json.load(json_data)
#             db_obj = {}
#             if file == "items.json":
#                 for obj in db_data:
#                     if obj.get("uid", -1) == obj_to_post.get("uid", -2):
#                         db_obj = obj
#             else:
#                 for obj in db_data:
#                     if obj.get("id", -1) == obj_to_post.get("id", -2):
#                         db_obj = obj
#             #pdb.set_trace()
#             #print("object_to_post", obj_to_post)
#             #print("db_obj", db_obj)
#             success = remove_timestamps(db_obj) == obj_to_post
#             #likely fails because POST updated the created_at and updated_at parameters
#             #AND/OR __contains__ compares identity which would return false even if parameters are the same

#             # print(response_data) run "python run_tests.py -s" in terminal to enable print
#             # success = response_data == db_data
#             results_file_name = "POST_" + file.split(".")[0]# + "_" + test_datetime
#             diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
#             with open(f"testing/results/POST/{test_datetime}/{results_file_name}.json", "w") as f:
#                 json.dump(diagnostics[results_file_name], f)

#             shutil.copyfile("testing/test_data_backup/" + file, "data/" + file) #restore file
#             #assert response.ok
#             #assert response.status_code == 201
#             #assert success
            

def test_post_client():
    test_post_one_endpoint("clients.json")
def test_post_inventories():
    test_post_one_endpoint("inventories.json")
def test_post_item_groups():
    test_post_one_endpoint("item_groups.json")
def test_post_item_lines():
    test_post_one_endpoint("item_lines.json")
def test_post_item_types():
    test_post_one_endpoint("item_types.json")
def test_post_items():
    test_post_one_endpoint("items.json")
def test_post_locations():
    test_post_one_endpoint("locations.json")
def test_post_orders():
    test_post_one_endpoint("orders.json")
def test_post_shipments():
    test_post_one_endpoint("shipments.json")
def test_post_suppliers():
    test_post_one_endpoint("suplliers.json")
def test_post_transfers():
    test_post_one_endpoint("transfers.json")
def test_post_warehouses():
    test_post_one_endpoint("warehouses.json")

def test_post_one_endpoint(file: string):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"POST_{file.split(".")[0]}_{test_datetime}" 
    os.makedirs(f"testing/results/POST", exist_ok=True)

    match file.split(".")[0]:
        case "clients":#todo
            obj_to_post = {"id": 999, "name": "Test_Client", "address": "Nowhere", "city": "Nowhere", "zip_code": "00000", "province": "Nowhere", "country": "Nowhere", "contact_name": "Test_Client", "contact_phone": "242.732.3483x2573", "contact_email": "test_client@example.net", "created_at": "", "updated_at": ""}
        case "inventories":#todo
            obj_to_post = {"id": 999, "item_id": "P000001", "description": "Face-to-face clear-thinking complexity", "item_reference": "sjQ23408K", "locations": [3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817], "total_on_hand": 262, "total_expected": 0, "total_ordered": 80, "total_allocated": 41, "total_available": 141, "created_at": "", "updated_at": ""}
        case "item_groups":#todo
            obj_to_post = {"id": 999, "name": "Electronics", "description": "", "created_at": "", "updated_at": ""}
        case "item_lines":#todo
            obj_to_post = {"id": 999, "name": "Tech Gadgets", "description": "", "created_at": "", "updated_at": ""}
        case "item_types":#todo
            obj_to_post = {"id": 999, "name": "Laptop", "description": "", "created_at": "", "updated_at": ""}
        case "items":#todo
            obj_to_post = {
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
                "created_at": "",
                "updated_at": ""
            }
        case "locations":#todo
            obj_to_post = {"id": 999, "warehouse_id": 1, "code": "A.1.0", "name": "Row: A, Rack: 1, Shelf: 0", "created_at": "", "updated_at": ""}
        case "orders":#todo
            obj_to_post = {
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
                "created_at": "",
                "updated_at": "",
                "items": [] #todo
            }
        case "shipments":#todo
            obj_to_post=  {
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
                "created_at": "",
                "updated_at": "",
                "items": [] #todo
            }
        case "suppliers":#todo
            obj_to_post = {"id": 999, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "", "updated_at": ""}
        case "transfers":#todo
            obj_to_post = {
                "id": 999,
                "reference": "TR00001",
                "transfer_from": None, #supposed to be null
                "transfer_to": 9229,
                "transfer_status": "Completed",
                "created_at": "",
                "updated_at": "",
                "items": [
                    {
                        "item_id": "P007435",
                        "amount": 23
                    }
                ]
            }
        case "warehouses":#todo
            obj_to_post = {"id": 999, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AS", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "", "updated_at": ""}


    #obj_to_post = post_objects[file.split(".")[0]]
    #print("object_to_post", obj_to_post)
    start = timer()
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
        db_obj = {}
        if file == "items.json":
            for obj in db_data:
                if obj.get("uid", -1) == obj_to_post.get("uid", -2):
                    db_obj = obj
        else:
            for obj in db_data:
                if obj.get("id", -1) == obj_to_post.get("id", -2):
                    db_obj = obj
        #pdb.set_trace()
        #print("object_to_post", obj_to_post)
        #print("db_obj", db_obj)
        success = remove_timestamps(db_obj) == obj_to_post
        #likely fails because POST updated the created_at and updated_at parameters
        #AND/OR __contains__ compares identity which would return false even if parameters are the same

        # print(response_data) run "python run_tests.py -s" in terminal to enable print
        # success = response_data == db_data
        # + "_" + test_datetime
        diagnostics = {}
        diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}

        with open(f"testing/results/POST/{results_file_name}.json", "w") as f:
            json.dump(diagnostics[results_file_name], f)

        shutil.copyfile("testing/test_data_backup/" + file, "data/" + file) #restore file

        assert success