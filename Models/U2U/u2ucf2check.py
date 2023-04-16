import pandas as pd
import os
from u2ucf2 import U2UCF2, lmap

current_dir = os.path.dirname(os.path.abspath(__file__))

df_ratings = pd.read_csv(current_dir + '/../Data/RatingsFromDb.csv')

ratings_list = lmap(
    lambda x: [int(x[0]), int(x[1]), x[2]],
    df_ratings.values.tolist()
)

reco = U2UCF2(ratings_list)

for i in range(10):
    print(reco.recommend(i))

print(reco.popular_list[:20])