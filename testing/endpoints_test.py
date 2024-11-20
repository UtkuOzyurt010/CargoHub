import requests
import json
import os
import datetime
from ..api.models import orders
from ..api.models import clients
from ..api.models import inventories
from ..api.models import item_groups
from ..api.models import item_lines
from ..api.models import item_types
from ..api.models import items
from ..api.models import locations
from ..api.models import shipments
from ..api.models import suppliers
from ..api.models import transfers
from ..api.models import warehouses



from timeit import default_timer as timer

address = "http://localhost:3000/api/v1"
dummies = [orders(), clients(), inventories(), item_groups(), item_lines(), item_types(),]
def test_get():
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M")
    os.makedirs("testing/results/GET/" + test_datetime)
    diagnostics = {}
    files = os.listdir("data")
    for file in files:
        start = timer()
        response = requests.get(address + "/" + file.split(".")[0], #remove .json from filename
                                headers=
                                {'API_KEY': 'a1b2c3d4e5',
                                'content-type': 'application/json'}
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
                
        assert success == True

def test_get_id():
    test_datetime = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M")
    os.makedirs("testing/results/GET_ID/" + test_datetime)
    diagnostics = {}
    files = os.listdir("data")
    for file in files:
        start = timer()
        response = requests.get(address + "/" + file.split(".")[0] + "/1", #remove .json from filename
                                headers=
                                {'API_KEY': 'a1b2c3d4e5',
                                'content-type': 'application/json'}
                                )
        
        response_time = timer() - start
        with open("data/" + file) as json_data:
            response_data = response.json()
            db_data = json.load(json_data)
            # print(response_data) run "python run_tests.py -s" in terminal to enable print
            success = response_data == db_data[0]
            results_file_name = "GET_ID_" + file.split(".")[0]# + "_" + test_datetime
            diagnostics[results_file_name] = {"succes" : success, "response_time" : response_time}
            
            with open(f"testing/results/GET_ID/{test_datetime}/{results_file_name}.json", "w") as f:
                json.dump(diagnostics[results_file_name], f)

        assert success == True

def test_del():

        