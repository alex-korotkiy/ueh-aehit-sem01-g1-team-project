import unittest
import pandas as pd
import os
from u2ucf import *

class RecommenderTests(unittest.TestCase):
    def __init__(self, methodName: str = "runTest") -> None:
        super().__init__(methodName)

        current_dir = os.path.dirname(os.path.abspath(__file__))

        df_users = pd.read_csv(current_dir + '/../Data/Users.csv')
        df_books = pd.read_csv(current_dir + '/../Data/Books.csv')
        df_ratings = pd.read_csv(current_dir + '/../Data/Ratings.csv')

        rating_median = 5

        users_index = {}
        items_index = {}

        users_list = df_users.values.tolist()
        books_list = df_books.values.tolist()

        for i in range(len(users_list)):
            users_index[users_list[i][0]] = i

        for i in range(len(books_list)):
            items_index[books_list[i][0]] = i


        ratings_list = df_ratings.values.tolist()


        user_ratings = lmap(
            lambda x: [],
            range(len(df_users))
        )

        transformed_count = 0

        for rating in ratings_list:
            user_index = users_index.get(rating[0], None)
            item_index = items_index.get(rating[1], None)
            if user_index is not None and item_index is not None:
                transformed_count +=1
                user_ratings[user_index].append([item_index, rating[2] - rating_median])


        self.recommender = U2UCF(user_ratings)


    def testRecommendation(self):
        recommendation = self.recommender.recommend(7)
        self.assertListEqual(recommendation, [3022, 19640, 17191, 20647, 19955, 3600, 37, 4543, 9753, 469, 90, 8229, 1150, 33591, 33997, 207232, 207267, 9577, 184440, 61729])



