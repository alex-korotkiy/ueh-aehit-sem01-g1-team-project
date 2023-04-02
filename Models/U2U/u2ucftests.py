import unittest
from u2ucf import *

class MetricTests(unittest.TestCase):

    def test_mapk(self):
        relevant = [
            [1, 7, 6, 2, 8],
            [1, 5, 4, 8],
            [8, 2, 5]
        ]

        pred = [
            [8, 1, 5, 0, 7, 2, 9, 4],
            [0, 1, 8, 5, 3, 4, 7, 9],
            [9, 2, 0, 6, 8, 5, 3, 7]
        ]

        self.assertEqual(round(mapk(relevant, pred, k=5), 4), 0.4331)
        

class MathTests(unittest.TestCase):
    
    def test_similarity_nz(self):
        ratings1 = {1: 1, 2: 1}
        ratings2 = {2: 1, 3: 1}
        self.assertEqual(round(similarity(ratings1, ratings2), 6), 0.5)

    def test_similarity_zero(self):
        ratings1 = {1: 1, 2: 1}
        ratings2 = {3: -1, 4: -1}
        self.assertEqual(similarity(ratings1, ratings2), 0)
        