import os, pathlib
import pytest
from .testing.endpoints_test import *

#os.chdir( pathlib.Path.cwd() / 'testing' )

#pytest.main()
#pytest.main(["--cov=. --cov-report=html endpoints_test.py"])

if __name__ == "__main__":
    test_get_all_clients()
    test_post_client()
    test_put_one_client()
    test_delete_one_client()