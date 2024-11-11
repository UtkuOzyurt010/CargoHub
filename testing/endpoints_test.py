import pytest

import requests
import json
import os # for making directories and reading their files
import shutil # for copying files
import datetime
import string
from timeit import default_timer as timer

class TestException(Exception):
    pass

#backup all data/ data
source_dir = "data/"
destination_dir = "testing/test_data_backup/"

os.makedirs(destination_dir, exist_ok=True)

# Iterate over all files in the source directory
for file_name in os.listdir(source_dir):
    full_file_path = os.path.join(source_dir, file_name)
    if os.path.isfile(full_file_path):  # Check if it is a file
        shutil.copyfile(full_file_path, os.path.join(destination_dir, file_name))



#dummydata for post tests
dummy_client = {"id": 999, "name": "Test_Client", "address": "Nowhere", "city": "Nowhere", "zip_code": "00000", "province": "Nowhere", "country": "Nowhere", "contact_name": "Test_Client", "contact_phone": "242.732.3483x2573", "contact_email": "test_client@example.net", "created_at": "", "updated_at": ""}
dummy_inventory = {"id": 999, "item_id": "P000001", "description": "Face-to-face clear-thinking complexity", "item_reference": "sjQ23408K", "locations": [3211, 24700, 14123, 19538, 31071, 24701, 11606, 11817], "total_on_hand": 262, "total_expected": 0, "total_ordered": 80, "total_allocated": 41, "total_available": 141, "created_at": "", "updated_at": ""}
dummy_item_group = {"id": 999, "name": "Electronics", "description": "", "created_at": "", "updated_at": ""}
dummy_item_line = {"id": 999, "name": "Tech Gadgets", "description": "", "created_at": "", "updated_at": ""}
dummy_item_type = {"id": 999, "name": "Laptop", "description": "", "created_at": "", "updated_at": ""}
dummy_item = {
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
dummy_location = {"id": 999, "warehouse_id": 1, "code": "A.1.0", "name": "Row: A, Rack: 1, Shelf: 0", "created_at": "", "updated_at": ""}
dummy_order = {
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
dummy_shipment = {
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
dummy_supplier = {"id": 999, "code": "SUP0001", "name": "Lee, Parks and Johnson", "address": "5989 Sullivan Drives", "address_extra": "Apt. 996", "city": "Port Anitaburgh", "zip_code": "91688", "province": "Illinois", "country": "Czech Republic", "contact_name": "Toni Barnett", "phonenumber": "363.541.7282x36825", "reference": "LPaJ-SUP0001", "created_at": "", "updated_at": ""}
dummy_transfer = {
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
dummy_warehouse = {"id": 999, "code": "YQZZNL56", "name": "Heemskerk cargo hub", "address": "Karlijndreef 281", "zip": "4002 AS", "city": "Heemskerk", "province": "Friesland", "country": "NL", "contact": {"name": "Fem Keijzer", "phone": "(078) 0013363", "email": "blamore@example.net"}, "created_at": "", "updated_at": ""}

#dummy parameters for put tests
dummy_put_test_id = "999"
id_test_value = 1000


def remove_timestamps(obj): #posting objects adds timestamps to them. We need to remove them when comparing
    newobj = dict.copy(obj)
    newobj["created_at"] = ""
    newobj["updated_at"] = ""
    return newobj


address = "http://localhost:3000/api/v1"
# def test_get():
#     test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
#     os.makedirs("testing/results/GET/" + test_datetime)
#     diagnostics = {}
#     files = os.listdir("data")
#     for file in files:
#         start = timer()
#         response = requests.get(address + "/" + file.split(".")[0], #remove .json from filename
#                                 headers=
#                                 {'API_KEY': 'a1b2c3d4e5'}
#                                 #'content-type': 'application/json'},
#                                 )
        
#         response_time = timer() - start
#         with open("data/" + file) as json_data: # loads eg entire clients.json  
#             response_data = response.json()
#             db_data = json.load(json_data)
#             # print(response_data) run "python run_tests.py -s" in terminal to enable print
#             success = response_data == db_data
#             #print(obj for obj in response_data if obj not in db_data)
#             results_file_name = "GET_" + file.split(".")[0]# + "_" + test_datetime
#             diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
#             with open(f"testing/results/GET/{test_datetime}/{results_file_name}.json", "w") as f:
#                 json.dump(diagnostics[results_file_name], f)
#             #assert response.ok
#             #assert success #keep this disabled until GET tests are seperated, 
#             #otherwise testing might stop before all endpoints are tested


# def test_get_all_clients(test_get_one_endpoint("clients.json")):
#     assert test_get_one_endpoint
def test_get_all_clients():
    test_get_endpoint("clients.json")
def test_get_all_inventories():
    test_get_endpoint("inventories.json")
def test_get_all_item_groups():
    test_get_endpoint("item_groups.json")
def test_get_all_item_lines():
    test_get_endpoint("item_lines.json")
def test_get_all_item_types():
    test_get_endpoint("item_types.json")
def test_get_all_items():
    test_get_endpoint("items.json")
def test_get_all_locations():
    test_get_endpoint("locations.json")
def test_get_all_orders():
    test_get_endpoint("orders.json")
def test_get_all_shipments():
    test_get_endpoint("shipments.json")
def test_get_all_suppliers():
    test_get_endpoint("suppliers.json")
def test_get_all_transfers():
    test_get_endpoint("transfers.json")
def test_get_all_warehouses():
    test_get_endpoint("warehouses.json")

#parameterization or something similar would be VERY useful here
#https://docs.pytest.org/en/stable/how-to/parametrize.html#parametrize-basics
def test_get_one_client():
    test_get_endpoint("clients.json", "1")
def test_get_one_inventory():
    test_get_endpoint("inventories.json", "1")
def test_get_one_item_group():
    test_get_endpoint("item_groups.json", "1")
def test_get_one_item_line():
    test_get_endpoint("item_lines.json", "1")
def test_get_one_item_type():
    test_get_endpoint("item_types.json", "1")
def test_get_one_item():
    test_get_endpoint("items.json", "P000001")
def test_get_one_location():
    test_get_endpoint("locations.json", "1")
def test_get_one_order():# fails because there's multiple orders with the same id
    test_get_endpoint("orders.json", "1")
def test_get_one_shipment():
    test_get_endpoint("shipments.json", "1")
def test_get_one_supplier():
    test_get_endpoint("suppliers.json", "1")
def test_get_one_transfer():
    test_get_endpoint("transfers.json", "1")
def test_get_one_warehouse():
    test_get_endpoint("warehouses.json", "1")



#@pytest.fixture
def test_get_endpoint(file: string, id: string=None):
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
            obj_to_post = dummy_client
        case "inventories":
            obj_to_post = dummy_inventory
        case "item_groups":
            obj_to_post = dummy_item_group
        case "item_lines":
            obj_to_post = dummy_item_line
        case "item_types":
            obj_to_post = dummy_item_type
        case "items":
            obj_to_post = dummy_item
        case "locations":
            obj_to_post = dummy_location
        case "orders":
            obj_to_post = dummy_order
        case "shipments":
            obj_to_post=  dummy_shipment
        case "suppliers":
            obj_to_post = dummy_supplier
        case "transfers":
            obj_to_post = dummy_transfer
        case "warehouses":
            obj_to_post = dummy_warehouse

    start = timer()
    #shutil.copyfile("data/" + file, "testing/test_data_backup/" + file) #backup original file
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

        #shutil.copyfile("testing/test_data_backup/" + file, "data/" + file) #restore file from backup

        assert success



# The API has the following UPDATE (PUT) endpoints.
# warehouses/{id}
# locations/{id}
# transfers/{id}
#   transfers/{id}/commit - Commits a transfer, adjusting inventory levels accordingly and updating the transfer status to "Processed".
# items/{id}
# item_lines/{id}
# item_groups/{id}
# item_types/{id}
# inventories/{id}
# suppliers/{id}
# orders/{id}
#   orders/{id}/items - Updates one (entire) items list
# clients/{id}
# shipments/{id}
#   shipments/{id}/orders - Updates one (entire) orders list
#   shipments/{id}/items - Updates one (entire) items list
#   shipments/{id}/commit - Placeholder for committing shipment changes (currently not implemented).

def test_put_one_warehouse():
    test_put_endpoint("warehouses.json", dummy_put_test_id)

def test_put_one_location():
    test_put_endpoint("locations.json", dummy_put_test_id)

def test_put_one_transfer():
    test_put_endpoint("transfers.json", dummy_put_test_id)

def test_put_one_item():
    test_put_endpoint("items.json", dummy_put_test_id)

def test_put_one_item_line():
    test_put_endpoint("item_lines.json", dummy_put_test_id)

def test_put_one_item_group():
    test_put_endpoint("item_groups.json", dummy_put_test_id)

def test_put_one_item_type():
    test_put_endpoint("item_types.json", dummy_put_test_id)

def test_put_one_inventory():
    test_put_endpoint("inventories.json", dummy_put_test_id)

def test_put_one_supplier():
    test_put_endpoint("suppliers.json", dummy_put_test_id)

def test_put_one_order():  # Note: this may fail due to duplicate IDs
    test_put_endpoint("orders.json", dummy_put_test_id)

def test_put_one_client():
    test_put_endpoint("clients.json", dummy_put_test_id)

def test_put_one_shipment():
    test_put_endpoint("shipments.json", dummy_put_test_id)


def test_put_endpoint(file: string, id: string):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"PUT_{file.split(".")[0]}_{test_datetime}" 
    os.makedirs(f"testing/results/PUT", exist_ok=True)

    match file.split(".")[0]:  # choose which object to update
        case "clients":
            obj_to_put = dummy_client
            obj_to_put["id"] = id_test_value
        case "inventories":
            obj_to_put = dummy_inventory
            obj_to_put["id"] = id_test_value
        case "item_groups":
            obj_to_put = dummy_item_group
            obj_to_put["id"] = id_test_value
        case "item_lines":
            obj_to_put = dummy_item_line
            obj_to_put["id"] = id_test_value
        case "item_types":
            obj_to_put = dummy_item_type
            obj_to_put["id"] = id_test_value
        case "items":
            obj_to_put = dummy_item
            obj_to_put["uid"] = str(id_test_value) #item has string UID
        case "locations":
            obj_to_put = dummy_location
            obj_to_put["id"] = id_test_value
        case "orders":
            obj_to_put = dummy_order
            obj_to_put["id"] = id_test_value
        case "shipments":
            obj_to_put = dummy_shipment
            obj_to_put["id"] = id_test_value
        case "suppliers":
            obj_to_put = dummy_supplier
            obj_to_put["id"] = id_test_value
        case "transfers":
            obj_to_put = dummy_transfer
            obj_to_put["id"] = id_test_value
        case "warehouses":
            obj_to_put = dummy_warehouse
            obj_to_put["id"] = id_test_value

    start = timer()
    #shutil.copyfile("data/" + file, "testing/test_data_backup/" + file) #backup original file
    response = requests.put(address + "/" + file.split(".")[0] + "/" + id, #remove .json from filename
                            json=obj_to_put,
                            headers=
                            {'API_KEY': 'a1b2c3d4e5',
                            'content-type': 'application/json'}
                                )
    response_time = timer() - start 
    #obj_to_put has "created_at" and "updated_at" properties set to ""
    #when obj_to_put is posted using a PUT call, our codebase adds a value to "created_at" and "updated_at"
    #we thus have to find the object in the database using only it's Id, then set these properties back to "" to be able to compare
    with open("data/" + file) as json_data:
        db_data = json.load(json_data)
        db_obj = {} #makes sure it's empty rather than null if obj isn't found, otherwise remove_timestamps() throws exception
        if file == "items.json": #because items uses a string uid rather than an int id
            for obj in db_data:
                if obj.get("uid", -1) == obj_to_put.get("uid", -2): # -1 and -2 as non-equal default return values in case of fail
                    db_obj = obj
        else:
            for obj in db_data:
                if obj.get("id", -1) == obj_to_put.get("id", -2):
                    db_obj = obj
        success = remove_timestamps(db_obj) == remove_timestamps(obj_to_put)

        diagnostics = {}
        diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}

        with open(f"testing/results/PUT/{results_file_name}.json", "w") as f:
            json.dump(diagnostics[results_file_name], f)

        #shutil.copyfile("testing/test_data_backup/" + file, "data/" + file) #restore file from backup

        assert success

# The API has the following DELETE endpoints.
# warehouses/{id}
# locations/{id}
# transfers/{id}
# items/{id}
# item_lines/{id}
# item_groups/{id}
# item_types/{id}
# inventories/{id}
# suppliers/{id}
# orders/{id}
# clients/{id}
# shipments/{id}

def test_delete_one_warehouse():
    test_delete_endpoint("warehouses.json", id_test_value)

def test_delete_one_location():
    test_delete_endpoint("locations.json", id_test_value)

def test_delete_one_transfer():
    test_delete_endpoint("transfers.json", id_test_value)

def test_delete_one_item():
    test_delete_endpoint("items.json", id_test_value)

def test_delete_one_item_line():
    test_delete_endpoint("item_lines.json", id_test_value)

def test_delete_one_item_group():
    test_delete_endpoint("item_groups.json", id_test_value)

def test_delete_one_item_type():
    test_delete_endpoint("item_types.json", id_test_value)

def test_delete_one_inventory():
    test_delete_endpoint("inventories.json", id_test_value)

def test_delete_one_supplier():
    test_delete_endpoint("suppliers.json", id_test_value)

def test_delete_one_order():  # Note: this may fail due to duplicate IDs
    test_delete_endpoint("orders.json", id_test_value)

def test_delete_one_client():
    test_delete_endpoint("clients.json", id_test_value)

def test_delete_one_shipment():
    test_delete_endpoint("shipments.json", id_test_value)


def test_delete_endpoint(file: string, id: string):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"DELETE_{file.split(".")[0]}_{id}_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/DELETE", exist_ok=True)
    
    with open("data/" + file) as json_data: # loads eg entire clients.json  
        db_data = json.load(json_data)
        found_obj = {}
        for obj in db_data:
            if obj.get("id", -1) == id or obj.get("uid", -1) == str(id):
                found_obj = obj

    #making sure the object doest indeed exist so we can actually try to delete
    if found_obj == {}:
        raise TestException("Test Exception: ID or UID does not exist.")
    

    start = timer()
    response = requests.delete(f"{address}/{file.split(".")[0]}/{id}", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})

    response_time = timer() - start
    with open("data/" + file) as json_data: # loads eg entire clients.json  
        db_data = json.load(json_data)
        found_obj = {}
        for obj in db_data:
            if str(obj.get("id", -1)) == id or str(obj.get("uid", -1)) == id:
                found_obj = id
        success = found_obj == {}
        
        diagnostics = {}
        diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
        
        with open(f"testing/results/DELETE/{results_file_name}.json", "w") as f:
            json.dump(diagnostics[results_file_name], f)
        #assert success #keep this disabled until GET tests are seperated, 
        #otherwise testing might stop before all endpoints are tested
        assert success


#restore initial data
# Define source and destination directories
backup_dir = "testing/test_data_backup/"
data_dir = "data/"

os.makedirs(data_dir, exist_ok=True)

# Iterate over all files in the backup directory
for file_name in os.listdir(backup_dir):
    full_backup_path = os.path.join(backup_dir, file_name)
    if os.path.isfile(full_backup_path):  # Check if it is a file
        shutil.copyfile(full_backup_path, os.path.join(data_dir, file_name))