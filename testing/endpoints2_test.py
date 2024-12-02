import pytest

import requests
import json
import os # for making directories and reading their files
import shutil # for copying files
import datetime
import string
from timeit import default_timer as timer

from testing.endpoints_test import loaddbdata, backuprestore, remove_timestamps
from testing.endpoints_test import dummy_client, dummy_inventory, dummy_item, dummy_item_group, dummy_item_line, dummy_item_type, dummy_location, dummy_order, dummy_shipment, dummy_supplier, dummy_transfer, dummy_warehouse
from testing.endpoints_test import id_test_value_dummy, id_test_value_get, id_test_value_put
from testing.endpoints_test import address
from testing.endpoints_test import post_one_endpoint #used in put_custom_endpoint()



# e.g GET warehouses/{id}/locations
def test_get_warehouses_id_locations():
    get_warehouses_id_locations(1)
def test_get_items_id_inventory():
    get_items_id_inventory("P000001")
def test_get_item_lines_id_items():
    get_custom_endpoint("item_lines.json", 1, "items")
def test_get_item_groups_id_items():
    get_custom_endpoint("item_groups.json", 1, "items")
def test_get_item_types_id_items():
    get_custom_endpoint("item_types.json", 1, "items")
def test_get_suppliers_id_items():
    get_suppliers_id_items(1)
def test_get_orders_id_items():
    get_orders_id_items(1) # fails because it gets the  WRONG order (since there's 2 with same id)
def test_get_clients_id_orders():
    get_clients_id_orders(1)
def test_get_shipments_id_orders():
    get_shipments_id_orders(1)
def test_get_shipments_id_items():
    get_shipments_id_items(1)
# def test_get_shipments_id_commit():
#     get_custom_endpoint("shipments.json", 1, "commit")
def test_get_transfers_id_items():
    get_transfers_id_items(1)

#specifically for /items/{id}/inventory/totals
def test_get_items_id_inventory_totals():
    get_items_id_inventory_totals("P000001")


def test_put_orders_id_items():
    put_custom_endpoint("orders.json", "items")
def test_put_shipments_id_orders():
    put_custom_endpoint("shipments.json", "orders")
def test_put_shipments_id_items():
    put_custom_endpoint("shipments.json", "items")
def test_put_shipments_id_commit(): # FAILS BECAUSE NOT YET IMPLEMENTED
    put_custom_endpoint("shipments.json", "commit")
def test_put_transfers_id_commit():
    put_custom_endpoint("transfers.json", "commit")



def get_warehouses_id_locations(warehouse_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_warehouses_id_locations_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()
    response = requests.get(f"{address}/warehouses/{warehouse_id}/locations", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()

    id_name = "warehouse_id"
    list_to_search = loaddbdata("locations.json", get_all=True)
    found_objects = [obj for obj in list_to_search if obj.get(id_name, -1) == warehouse_id]
    success = response_data == found_objects
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success

def get_items_id_inventory(item_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_items_id_inventory_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()
    response = requests.get(f"{address}/items/{item_id}/inventory", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()

    id_name = "item_id"
    list_to_search = loaddbdata("inventories.json", get_all=True)
    found_objects = [obj for obj in list_to_search if obj.get(id_name, -1) == item_id]
    success = response_data == found_objects
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success


def get_custom_endpoint(file : string, id, listName : string): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_{file.split(".")[0]}_{id}_{listName}_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    response = requests.get(f"{address}/{file.split(".")[0]}/{id}/{listName}", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()

    id_name = f"{file[:-6]}_id"
    match file:
        case "item_lines.json":
            id_name = "item_line"
        case "item_types.json":
            id_name = "item_type"
        case "item_groups.json":
            id_name = "item_group"
    if file == "orders.json":
        list_to_search = loaddbdata("orders.json", id)
        found_objects = list_to_search.get("items")
    if listName == "inventory":
        list_to_search = loaddbdata(f"inventories.json", get_all=True)
        found_objects = [obj for obj in list_to_search if obj.get(id_name, -1) == id]
    else:
        list_to_search = loaddbdata(f"{listName}.json", get_all=True)
        found_objects = [obj for obj in list_to_search if obj.get(id_name, -1) == id]

    success = response_data == found_objects
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    #assert success #keep this disabled until GET tests are seperated, 
    #otherwise testing might stop before all endpoints are tested
    assert success

def get_suppliers_id_items(supplier_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_suppliers_id_items_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()
    response = requests.get(f"{address}/suppliers/{supplier_id}/items", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()

    id_name = "supplier_id"
    list_to_search = loaddbdata("items.json", get_all=True)
    found_objects = [obj for obj in list_to_search if obj.get(id_name, -1) == supplier_id]
    success = response_data == found_objects
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success


def get_orders_id_items(order_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_orders_id_items_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    response = requests.get(f"{address}/orders/{order_id}/items", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()
    
    list_to_search = loaddbdata("orders.json", order_id)
    found_objects = list_to_search.get("items", -1)

    success = response_data == found_objects
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success

def get_clients_id_orders(client_id):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_clients_id_orders_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()
    response = requests.get(f"{address}/clients/{client_id}/orders", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()

    id_name1 = "ship_to"
    id_name2 = "bill_to"
    list_to_search = loaddbdata("orders.json", get_all=True)
    found_objects = [obj for obj in list_to_search if obj.get(id_name1, -1) == client_id or obj.get(id_name2, -1) == client_id]
    success = response_data == found_objects
    
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success


def get_shipments_id_orders(shipment_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_shipments_id_orders_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    response = requests.get(f"{address}/shipments/{shipment_id}/orders", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json() # for some reason this is [1, 999, 999, 999, 999]
    # but in shipments.json at object with shipment_id 1, items contains
    #[{"item_id": "P007435", "amount": 23}, {"item_id": "P009557", "amount": 1}, {"item_id": "P009553", "amount": 50}, {"item_id": "P010015", "amount": 16}, {"item_id": "P002084", "amount": 33}, {"item_id": "P009663", "amount": 18}, {"item_id": "P010125", "amount": 18}, {"item_id": "P005768", "amount": 26}, {"item_id": "P004051", "amount": 1}, {"item_id": "P005026", "amount": 29}, {"item_id": "P000726", "amount": 22}, {"item_id": "P008107", "amount": 47}, {"item_id": "P001598", "amount": 32}, {"item_id": "P002855", "amount": 20}, {"item_id": "P010404", "amount": 30}, {"item_id": "P010446", "amount": 6}, {"item_id": "P001517", "amount": 9}, {"item_id": "P009265", "amount": 2}, {"item_id": "P001108", "amount": 20}, {"item_id": "P009110", "amount": 18}, {"item_id": "P009686", "amount": 13}]
    id_name = "shipment_id"
    list_to_search = loaddbdata("orders.json", get_all=True)
    found_objects = [obj.get("id", -1) for obj in list_to_search if obj.get(id_name, -1) == shipment_id]

    success = response_data == found_objects
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success

def get_shipments_id_items(shipment_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_shipments_id_items_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    response = requests.get(f"{address}/shipments/{shipment_id}/items", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()
    
    list_to_search = loaddbdata("shipments.json", shipment_id)
    found_objects = list_to_search.get("items", -1)

    success = response_data == found_objects
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success


def get_shipments_id_commit(shipment_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    #this endpoint isn't even implemented yet
    pass


def get_transfers_id_items(transfer_id): #listName is e.g. "items" or "orders" for e.g. "/suppliers/{id}/items"
    
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_transfers_id_items_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    response = requests.get(f"{address}/transfers/{transfer_id}/items", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()
    
    list_to_search = loaddbdata("transfers.json", transfer_id)
    found_objects = list_to_search.get("items", -1)

    success = response_data == found_objects
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success



def get_items_id_inventory_totals(item_id):
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"GET_items_{item_id}_inventory_totals_{test_datetime}" #ternary here REQUIRES else
    os.makedirs("testing/results/GET", exist_ok=True)
    start = timer()

    response = requests.get(f"{address}/items/{item_id}/inventory/totals", #will this work with uid in items.json?
                            headers=
                            {'API_KEY': 'a1b2c3d4e5'})
    response_time = timer() - start
    response_data = response.json()

    list_to_search = loaddbdata("inventories.json", get_all=True)
    found_object = [obj for obj in list_to_search if obj.get("item_id", -1) == item_id][0]
    totals_dict = {}
    totals_dict["total_expected"] = found_object.get("total_expected", -1)
    totals_dict["total_ordered"] = found_object.get("total_ordered", -1)
    totals_dict["total_allocated"] = found_object.get("total_allocated", -1)
    totals_dict["total_available"] = found_object.get("total_available", -1)

    success = response_data == totals_dict
    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
    
    with open(f"testing/results/GET/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    #assert success #keep this disabled until GET tests are seperated, 
    #otherwise testing might stop before all endpoints are tested
    assert success


def put_custom_endpoint(file : string, listName : string):
    post_one_endpoint(file) # add dummy item to databse (has id 999)
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    results_file_name = f"PUT_{file.split(".")[0]}_1000_{listName}_{test_datetime}" 
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
    response = requests.put(f"{address}/{file.split(".")[0]}/{str(id_test_value_dummy)}/{listName}", #remove .json from filename
                            json=obj_to_put,
                            headers=
                            {'API_KEY': 'a1b2c3d4e5',
                            'content-type': 'application/json'})
    response_time = timer() - start

    found_obj = loaddbdata(file, id_test_value_put)
    success = remove_timestamps(found_obj) == remove_timestamps(obj_to_put)

    diagnostics = {}
    diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}

    with open(f"testing/results/PUT/{results_file_name}.json", "w") as f:
        json.dump(diagnostics[results_file_name], f)
    assert success