import pytest


from api.main import func_to_test

# @pytest.fixture
def test_func_to_test():
    assert func_to_test(2,3) == 5
