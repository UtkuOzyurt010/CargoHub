import os, pathlib
import pytest

#os.chdir( pathlib.Path.cwd() / 'testing' )

#pytest.main()
pytest.main(["--cov=. --cov-report=html endpoints_test.py"])
