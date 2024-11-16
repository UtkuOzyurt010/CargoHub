import pytest

import requests
import json
import os # for making directories and reading their files
import shutil # for copying files
import datetime
import string
from timeit import default_timer as timer

#have our own Exception to be able to raise with informative message
class TestException(Exception):
    pass


def loaddbdata(file: string, id: string=None, get_all: bool=False):
    if get_all is True:
        with open("testing/test_data/" + file) as json_data: # loads eg entire clients.json  
            db_data = json.load(json_data)
        return db_data
    
    with open("testing/test_data/" + file) as json_data:
        db_data = json.load(json_data)
        db_obj = {} #makes sure it's empty rather than null if obj isn't found, otherwise remove_timestamps() throws exception
        if file == "items.json": #because items uses a string uid rather than an int id
            for obj in db_data:
                if obj.get("uid", -1) == id: # -1 and -2 as non-equal default return values in case of fail
                    db_obj = obj
        else:
            for obj in db_data:
                if obj.get("id", -1) == id:
                    db_obj = obj
    return db_obj


def backuprestore(restore: bool=False):
    source_dir = "testing/test_data/"

    if restore is False:
        #backup all data/ data
        
        destination_dir = "testing/test_data_backup/"

        os.makedirs(destination_dir, exist_ok=True)

        # Iterate over all files in the source directory
        for file_name in os.listdir(source_dir):
            full_file_path = os.path.join(source_dir, file_name)
            if os.path.isfile(full_file_path):  # Check if it is a file
                shutil.copyfile(full_file_path, os.path.join(destination_dir, file_name))
    elif restore is True:
        #restore initial data
        # Define source and destination directories
        backup_dir = "testing/test_data_backup/"

        os.makedirs(source_dir, exist_ok=True)

        # Iterate over all files in the backup directory
        for file_name in os.listdir(backup_dir):
            full_backup_path = os.path.join(backup_dir, file_name)
            if os.path.isfile(full_backup_path):  # Check if it is a file
                shutil.copyfile(full_backup_path, os.path.join(source_dir, file_name))

def remove_timestamps(obj): #posting objects adds timestamps to them. We need to remove them when comparing
    newobj = dict.copy(obj)
    newobj["created_at"] = ""
    newobj["updated_at"] = ""
    return newobj


#test_backuprestore()

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
                "transfer_status": "Scheduled",
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
id_test_value_put = 1000
id_test_value_get = 1
id_test_value_dummy = 999






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
#         with open("testing/test_data/" + file) as json_data: # loads eg entire clients.json  
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
    get_endpoint("clients.json")
def test_get_all_inventories():
    get_endpoint("inventories.json")
def test_get_all_item_groups():
    get_endpoint("item_groups.json")
def test_get_all_item_lines():
    get_endpoint("item_lines.json")
def test_get_all_item_types():
    get_endpoint("item_types.json")
def test_get_all_items():
    get_endpoint("items.json")
def test_get_all_locations():
    get_endpoint("locations.json")
def test_get_all_orders():
    get_endpoint("orders.json")
def test_get_all_shipments():
    get_endpoint("shipments.json")
def test_get_all_suppliers():
    get_endpoint("suppliers.json")
def test_get_all_transfers():
    get_endpoint("transfers.json")
def test_get_all_warehouses():
    get_endpoint("warehouses.json")

#parameterization or something similar would be VERY useful here
#https://docs.pytest.org/en/stable/how-to/parametrize.html#parametrize-basics
def test_get_one_client():
    get_endpoint("clients.json", id_test_value_get)
def test_get_one_inventory():
    get_endpoint("inventories.json", id_test_value_get)
def test_get_one_item_group():
    get_endpoint("item_groups.json", id_test_value_get)
def test_get_one_item_line():
    get_endpoint("item_lines.json", id_test_value_get)
def test_get_one_item_type():
    get_endpoint("item_types.json", id_test_value_get)
def test_get_one_item():
    get_endpoint("items.json", "P000001")
def test_get_one_location():
    get_endpoint("locations.json", id_test_value_get)
def test_get_one_order():# fails because there's multiple orders with the same id
    get_endpoint("orders.json", id_test_value_get)
def test_get_one_shipment():
    get_endpoint("shipments.json", id_test_value_get)
def test_get_one_supplier():
    get_endpoint("suppliers.json", id_test_value_get)
def test_get_one_transfer():
    get_endpoint("transfers.json", id_test_value_get)
def test_get_one_warehouse():
    get_endpoint("warehouses.json", id_test_value_get)



#@pytest.mark.skip(reason="This function is used as a helper, not for direct test runs.")
def get_endpoint(file: string, id=None):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_{file.split(".")[0]}_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    if(id==None):
        response = requests.get(address + "/" + file.split(".")[0], #remove .json from filename
                                headers=
                                {'API_KEY': 'a1b2c3d4e5'})
        
        response_time = timer() - start
        response_data = response.json()
        db_data = loaddbdata(file, get_all=True)

        success = response_data == db_data
        
        diagnostics = {}
        diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
        
        with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
            json.dump(diagnostics[results_file_name], f)
            #assert success #keep this disabled until GET tests are seperated, 
            #otherwise testing might stop before all endpoints are tested
    else:
        response = requests.get(f"{address}/{file.split(".")[0]}/{id}", #will this work with uid in items.json?
                                headers=
                                {'API_KEY': 'a1b2c3d4e5'})
        
        response_time = timer() - start

        response_data = response.json()

        found_obj = loaddbdata(file, id)

        success = response_data == found_obj
        
        diagnostics = {}
        diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
        
        with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
            json.dump(diagnostics[results_file_name], f)
        #assert success #keep this disabled until GET tests are seperated, 
        #otherwise testing might stop before all endpoints are tested
        assert success
        






def test_post_client():
    post_one_endpoint("clients.json")
def test_post_inventories():
    post_one_endpoint("inventories.json")
def test_post_item_groups():
    post_one_endpoint("item_groups.json")
def test_post_item_lines():
    post_one_endpoint("item_lines.json")
def test_post_item_types():
    post_one_endpoint("item_types.json")
def test_post_items():
    post_one_endpoint("items.json")
def test_post_locations():
    post_one_endpoint("locations.json")
def test_post_orders():
    post_one_endpoint("orders.json")
def test_post_shipments():
    post_one_endpoint("shipments.json")
def test_post_suppliers():
    post_one_endpoint("suppliers.json")
def test_post_transfers():
    post_one_endpoint("transfers.json")
def test_post_warehouses():
    post_one_endpoint("warehouses.json")


#pytest.mark.skip(reason="This function is used as a helper, not for direct test runs.")
def post_one_endpoint(file: string):
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
    #shutil.copyfile("testing/test_data/" + file, "testing/test_data_backup/" + file) #backup original file
    response = requests.post(address + "/" + file.split(".")[0], #remove .json from filename
                            json=obj_to_post,
                            headers=
                            {'API_KEY': 'a1b2c3d4e5',
                            'content-type': 'application/json'}
                            )
    response_time = timer() - start

    if file == "items.json":
        found_obj = loaddbdata(file, obj_to_post.get("uid", -1))
    else:
        found_obj = loaddbdata(file, obj_to_post.get("id", -1))


    print(response.headers)
    print(response.status_code)
    print(address + "/" + file.split(".")[0])
    print(remove_timestamps(found_obj))
    print(obj_to_post)
    success = remove_timestamps(found_obj) == obj_to_post
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}

    with open(f"testing/results/POST/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)

    #shutil.copyfile("testing/test_data_backup/" + file, "testing/test_data/" + file) #restore file from backup

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
    put_endpoint("warehouses.json")

def test_put_one_location():
    put_endpoint("locations.json")

def test_put_one_transfer():
    put_endpoint("transfers.json")

def test_put_one_item():
    put_endpoint("items.json")

def test_put_one_item_line():
    put_endpoint("item_lines.json")

def test_put_one_item_group():
    put_endpoint("item_groups.json")

def test_put_one_item_type():
    put_endpoint("item_types.json")

def test_put_one_inventory():
    put_endpoint("inventories.json")

def test_put_one_supplier():
    put_endpoint("suppliers.json")

def test_put_one_order():  # Note: this may fail due to duplicate IDs
    put_endpoint("orders.json")

def test_put_one_client():
    put_endpoint("clients.json")

def test_put_one_shipment():
    put_endpoint("shipments.json")

#@pytest.mark.skip(reason="This function is used as a helper, not for direct test runs.")
def put_endpoint(file: string):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"PUT_{file.split(".")[0]}_{test_datetime}" 
    os.makedirs(f"testing/results/PUT", exist_ok=True)

    match file.split(".")[0]:  # choose which object to update
        case "clients":
            obj_to_put = dummy_client
            obj_to_put["id"] = id_test_value_put
        case "inventories":
            obj_to_put = dummy_inventory
            obj_to_put["id"] = id_test_value_put
        case "item_groups":
            obj_to_put = dummy_item_group
            obj_to_put["id"] = id_test_value_put
        case "item_lines":
            obj_to_put = dummy_item_line
            obj_to_put["id"] = id_test_value_put
        case "item_types":
            obj_to_put = dummy_item_type
            obj_to_put["id"] = id_test_value_put
        case "items":
            obj_to_put = dummy_item
            obj_to_put["uid"] = str(id_test_value_put) #item has string UID
        case "locations":
            obj_to_put = dummy_location
            obj_to_put["id"] = id_test_value_put
        case "orders":
            obj_to_put = dummy_order
            obj_to_put["id"] = id_test_value_put
        case "shipments":
            obj_to_put = dummy_shipment
            obj_to_put["id"] = id_test_value_put
        case "suppliers":
            obj_to_put = dummy_supplier
            obj_to_put["id"] = id_test_value_put
        case "transfers":
            obj_to_put = dummy_transfer
            obj_to_put["id"] = id_test_value_put
        case "warehouses":
            obj_to_put = dummy_warehouse
            obj_to_put["id"] = id_test_value_put

    start = timer()
    #shutil.copyfile("testing/test_data/" + file, "testing/test_data_backup/" + file) #backup original file
    response = requests.put(address + "/" + file.split(".")[0] + "/" + str(id_test_value_dummy), #remove .json from filename
                            json=obj_to_put,
                            headers=
                            {'API_KEY': 'a1b2c3d4e5',
                            'content-type': 'application/json'}
                                )
    response_time = timer() - start

    
    found_obj = loaddbdata(file, id_test_value_put)

    success = remove_timestamps(found_obj) == remove_timestamps(obj_to_put)

    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}

    with open(f"testing/results/PUT/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)

    #shutil.copyfile("testing/test_data_backup/" + file, "testing/test_data/" + file) #restore file from backup

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
    delete_endpoint("warehouses.json", id_test_value_put)

def test_delete_one_location():
    delete_endpoint("locations.json", id_test_value_put)

def test_delete_one_transfer():
    delete_endpoint("transfers.json", id_test_value_put)

def test_delete_one_item():
    delete_endpoint("items.json", id_test_value_put)

def test_delete_one_item_line():
    delete_endpoint("item_lines.json", id_test_value_put)

def test_delete_one_item_group():
    delete_endpoint("item_groups.json", id_test_value_put)

def test_delete_one_item_type():
    delete_endpoint("item_types.json", id_test_value_put)

def test_delete_one_inventory():
    delete_endpoint("inventories.json", id_test_value_put)

def test_delete_one_supplier():
    delete_endpoint("suppliers.json", id_test_value_put)

def test_delete_one_order():  # Note: this may fail due to duplicate IDs
    delete_endpoint("orders.json", id_test_value_put)

def test_delete_one_client():
    delete_endpoint("clients.json", id_test_value_put)

def test_delete_one_shipment():
    delete_endpoint("shipments.json", id_test_value_put)


#@pytest.mark.skip(reason="This function is used as a helper, not for direct test runs.")
def delete_endpoint(file: string, id: int):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"DELETE_{file.split(".")[0]}_{id}_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/DELETE", exist_ok=True)
    
    found_obj = loaddbdata(file, id)

    #making sure the object doest indeed exist so we can actually try to delete
    if found_obj == {}:
        raise TestException("Test Exception: ID or UID does not exist.")
    

    start = timer()
    response = requests.delete(f"{address}/{file.split(".")[0]}/{id}", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})

    response_time = timer() - start
    
    found_obj = loaddbdata(file, id)
    
    success = found_obj == {}
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/DELETE/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    #assert success #keep this disabled until GET tests are seperated, 
    #otherwise testing might stop before all endpoints are tested
    assert success


#test_backuprestore(True)

