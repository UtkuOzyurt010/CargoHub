import pytest

import requests
import json
import os # for making directories and reading their files
import shutil # for copying files
import datetime
import string
from timeit import default_timer as timer

def remove_timestamps(obj): #posting objects adds timestamps to them. We need to remove them when comparing
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
                                #'content-type': 'application/json'},
                                )
        
        response_time = timer() - start
        with open("data/" + file) as json_data: # loads eg entire clients.json  
            response_data = response.json()
            db_data = json.load(json_data)
            # print(response_data) run "python run_tests.py -s" in terminal to enable print
            success = response_data == db_data
            #print(obj for obj in response_data if obj not in db_data)
            results_file_name = "GET_" + file.split(".")[0]# + "_" + test_datetime
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/GET/{test_datetime}/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)
            #assert response.ok
            #assert success #keep this disabled until GET tests are seperated, 
            #otherwise testing might stop before all endpoints are tested


# def test_get_all_clients(test_get_one_endpoint("clients.json")):
#     assert test_get_one_endpoint
def test_get_all_clients():
    test_get_one_endpoint("clients.json")
def test_get_all_inventories():
    test_get_one_endpoint("inventories.json")
def test_get_all_item_groups():
    test_get_one_endpoint("item_groups.json")
def test_get_all_item_lines():
    test_get_one_endpoint("item_lines.json")
def test_get_all_item_types():
    test_get_one_endpoint("item_types.json")
def test_get_all_items():
    test_get_one_endpoint("items.json")
def test_get_all_locations():
    test_get_one_endpoint("locations.json")
def test_get_all_orders():
    test_get_one_endpoint("orders.json")
def test_get_all_shipments():
    test_get_one_endpoint("shipments.json")
def test_get_all_suppliers():
    test_get_one_endpoint("suppliers.json")
def test_get_all_transfers():
    test_get_one_endpoint("transfers.json")
def test_get_all_warehouses():
    test_get_one_endpoint("warehouses.json")

#parameterization or something similar would be VERY useful here
#https://docs.pytest.org/en/stable/how-to/parametrize.html#parametrize-basics
def test_get_one_client():
    test_get_one_endpoint("clients.json", "1")
def test_get_one_inventory():
    test_get_one_endpoint("inventories.json", "1")
def test_get_one_item_group():
    test_get_one_endpoint("item_groups.json", "1")
def test_get_one_item_line():
    test_get_one_endpoint("item_lines.json", "1")
def test_get_one_item_type():
    test_get_one_endpoint("item_types.json", "1")
def test_get_one_item():
    test_get_one_endpoint("items.json", "P000001")
def test_get_one_location():
    test_get_one_endpoint("locations.json", "1")
def test_get_one_order():# fails because there's multiple orders with the same id
    test_get_one_endpoint("orders.json", "1")
def test_get_one_shipment():
    test_get_one_endpoint("shipments.json", "1")
def test_get_one_supplier():
    test_get_one_endpoint("suppliers.json", "1")
def test_get_one_transfer():
    test_get_one_endpoint("transfers.json", "1")
def test_get_one_warehouse():
    test_get_one_endpoint("warehouses.json", "1")



#@pytest.fixture
def test_get_one_endpoint(file: string, id: string=None):
    if(id==None):
        test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
        results_file_name = f"GET_{file.split(".")[0]}_{test_datetime}" #ternary here REQUIRES else
        os.makedirs("testing/results/GET", exist_ok=True)
        
        start = timer()
        response = requests.get(address + "/" + file.split(".")[0], #remove .json from filename
                                headers=
                                {'API_KEY': 'a1b2c3d4e5'})
        
        response_time = timer() - start
        with open("data/" + file) as json_data: # loads eg entire clients.json  
            response_data = response.json()
            db_data = json.load(json_data)

            success = response_data == db_data
            
            diagnostics = {}
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)
            #assert success #keep this disabled until GET tests are seperated, 
            #otherwise testing might stop before all endpoints are tested
    else:
        test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
        results_file_name = f"GET_{file.split(".")[0]}_{id}_{test_datetime}" #ternary here REQUIRES else
        os.makedirs("testing/results/GET", exist_ok=True)
        
        start = timer()
        response = requests.get(f"{address}/{file.split(".")[0]}/{id}", #will this work with uid in items.json?
                                headers=
                                {'API_KEY': 'a1b2c3d4e5'})
        
        response_time = timer() - start
        with open("data/" + file) as json_data: # loads eg entire clients.json  
            response_data = response.json()
            db_data = json.load(json_data)
            found_obj = {}
            for obj in db_data:
                if str(obj.get("id", -1)) == id or str(obj.get("uid", -1)) == id:
                    found_obj = obj
            success = response_data == found_obj
            
            diagnostics = {}
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)
            #assert success #keep this disabled until GET tests are seperated, 
            #otherwise testing might stop before all endpoints are tested
            assert success






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
    test_post_one_endpoint("suppliers.json")
def test_post_transfers():
    test_post_one_endpoint("transfers.json")
def test_post_warehouses():
    test_post_one_endpoint("warehouses.json")

#@pytest.fixture
def test_post_one_endpoint(file: string):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"POST_{file.split(".")[0]}_{test_datetime}" 
    os.makedirs(f"testing/results/POST", exist_ok=True)

    match file.split(".")[0]: #choose which object to add
        case "clients":
            obj_to_post = {"id": 999, "name": "Test_Client", "address": "Nowhere", "city": "Nowhere", "zip_code": "00000", "province": "Nowhere", "country": "Nowhere", "contact_name": "Test_Client", "contact_phone": "242.732.3483x2573", "contact_email": "test_client@example.net", "created_at": "", "updated_at": ""}
        case "inventories":
            obj_to_post = {"id": 999, "item_id": "P000001", "description": "Face-to-face clear-thinking complexity", "item_reference": "sjQ23408K", "locations": [3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817], "total_on_hand": 262, "total_expected": 0, "total_ordered": 80, "total_allocated": 41, "total_available": 141, "created_at": "", "updated_at": ""}
        case "item_groups":
            obj_to_post = {"id": 999, "name": "Electronics", "description": "", "created_at": "", "updated_at": ""}
        case "item_lines":
            obj_to_post = {"id": 999, "name": "Tech Gadgets", "description": "", "created_at": "", "updated_at": ""}
        case "item_types":
            obj_to_post = {"id": 999, "name": "Laptop", "description": "", "created_at": "", "updated_at": ""}
        case "items":
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
        case "locations":
            obj_to_post = {"id": 999, "warehouse_id": 1, "code": "A.1.0", "name": "Row: A, Rack: 1, Shelf: 0", "created_at": "", "updated_at": ""}
        case "orders":
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
                "items": []
            }
        case "shipments":
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
                "items": [] 
            }
        case "suppliers":
            obj_to_post = {"id": 999, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "", "updated_at": ""}
        case "transfers":
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
        case "warehouses":
            obj_to_post = {"id": 999, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AS", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "", "updated_at": ""}

    start = timer()
    shutil.copyfile("data/" + file, "testing/test_data_backup/" + file) #backup original file
    response = requests.post(address + "/" + file.split(".")[0], #remove .json from filename
                            json=obj_to_post,
                            headers=
                            {'API_KEY': 'a1b2c3d4e5',
                            'content-type': 'application/json'}
                            )
    response_time = timer() - start 

    #obj_to_post has "created_at" and "updated_at" properties set to ""
    #when obj_to_post is posted using a POST call, our codebase adds a value to "created_at" and "updated_at"
    #we thus have to find the object in the database using only it's Id, then set these properties back to "" to be able to compare
    with open("data/" + file) as json_data:
        db_data = json.load(json_data)
        db_obj = {} #makes sure it's empty rather than null if obj isn't found, otherwise remove_timestamps() throws exception
        if file == "items.json": #because items uses a string uid rather than an int id
            for obj in db_data:
                if obj.get("uid", -1) == obj_to_post.get("uid", -2): # -1 and -2 as non-equal default return values in case of fail
                    db_obj = obj
        else:
            for obj in db_data:
                if obj.get("id", -1) == obj_to_post.get("id", -2):
                    db_obj = obj
        success = remove_timestamps(db_obj) == obj_to_post
        diagnostics = {}
        diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}

        with open(f"testing/results/POST/{results_file_name}.json", "w") as f:
            json.dump(diagnostics[results_file_name], f)

        shutil.copyfile("testing/test_data_backup/" + file, "data/" + file) #restore file from backup

        assert success
