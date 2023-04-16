import pandas as pd
import pyodbc
from u2ucf2 import lmap, U2UCF2

conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=(local);'
                      'Database=BooksModel;'
                      'Trusted_Connection=yes;')

query = "SELECT UserId, ItemId, Rating FROM Ratings ORDER BY UserId, ItemId"
df_ratings = pd.read_sql(query, conn)
ratings_list = df_ratings.values.tolist()

recommender = U2UCF2(ratings_list)

cursor = conn.cursor()

def process_user(user_id):
    recommendations = recommender.recommend(user_id)
    insertValuesList = lmap(
        lambda item_id: f"({str(int(user_id))}, {str(int(item_id))})",
        recommendations
    )

    cursor.execute("DELETE UserRecommendations WHERE UserId=?", user_id)
    if(len(recommendations) > 0):
        cursor.execute(f"INSERT UserRecommendations (UserId, ItemId) VALUES {','.join(insertValuesList)}")
    
    cursor.commit()


for user_id in sorted(recommender.ratings.keys()):
    print(f"updating recommendations for user: {int(user_id)}")
    process_user(user_id)


cursor.close()