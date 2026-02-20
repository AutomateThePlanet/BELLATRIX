from flask import Flask, request, jsonify
from sentence_transformers import SentenceTransformer
import base64
import numpy as np

model = SentenceTransformer('nomic-ai/nomic-embed-text-v1', trust_remote_code=True)
app = Flask(__name__)

@app.route('/v1/embeddings', methods=['POST'])
def embed():
    input_data = request.json['input']
    vectors = model.encode(input_data)
    
    # Convert to base64 string (OpenAI-compatible)
    base64_vectors = []
    for vec in vectors:
        float_array = np.array(vec, dtype=np.float32)
        base64_string = base64.b64encode(float_array.tobytes()).decode("utf-8")
        base64_vectors.append({"embedding": base64_string})
    
    return jsonify({"data": base64_vectors})

app.run(port=11435)
