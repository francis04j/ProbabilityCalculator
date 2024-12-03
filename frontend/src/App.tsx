import React, { useState } from 'react';
import { Calculator, AlertCircle } from 'lucide-react';
import { Operation, calculateProbability } from './api/probability';
import { ProbabilityInput } from './components/ProbabilityInput';
import { OperationSelect } from './components/OperationSelect';



function App() {
  const [probA, setProbA] = useState<string>('');
  const [probB, setProbB] = useState<string>('');
  const [operation, setOperation] = useState<Operation>('combinedWith');
  const [error, setError] = useState<string>('');
  const [result, setResult] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);

  const validateProbability = (value: string): boolean => {
    const num = parseFloat(value);
    return !isNaN(num) && num >= 0 && num <= 1;
  };

  const handleInputChange = (value: string, setter: (value: string) => void) => {
    setter(value);
    if (value && !validateProbability(value)) {
      setError('Probabilities must be between 0 and 1');
    } else {
      setError('');
    }
    setResult(null);
  };

  const handleCalculate = async () => {
    if (!probA || !probB || !validateProbability(probA) || !validateProbability(probB)) {
      setError('Please enter valid probabilities');
      return;
    }

    try {
      setLoading(true);
      setError('');
      const response = await calculateProbability({ probA, probB, operation });
      setResult(response.result);
    } catch (err) {
      console.log(err)
      setError('Failed to calculate probability. Please try again.');
      setResult(null);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-50 flex items-center justify-center p-4">
      <div className="bg-white rounded-2xl shadow-xl p-8 w-full max-w-md">
        <div className="flex items-center gap-3 mb-6">
          <Calculator className="w-8 h-8 text-indigo-600" />
          <h1 className="text-2xl font-bold text-gray-800">Probability Calculator</h1>
        </div>

        <div className="space-y-6">
          <div className="space-y-4">
            <ProbabilityInput
              id="probA"
              label="Probability A"
              value={probA}
              onChange={(value) => handleInputChange(value, setProbA)}
            />
            <ProbabilityInput
              id="probB"
              label="Probability B"
              value={probB}
              onChange={(value) => handleInputChange(value, setProbB)}
            />
            <OperationSelect value={operation} onChange={setOperation} />
          </div>

          {error && (
            <div className="flex items-center gap-2 text-red-600 bg-red-50 p-3 rounded-lg">
              <AlertCircle className="w-5 h-5" />
              <p className="text-sm">{error}</p>
            </div>
          )}

          <button
            onClick={handleCalculate}
            disabled={loading || !probA || !probB || !!error}
            className="w-full py-2 px-4 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
          >
            {loading ? 'Calculating...' : 'Calculate'}
          </button>

          <div className="bg-gray-50 rounded-lg p-4">
            <p className="text-sm font-medium text-gray-600 mb-1">Result</p>
            <p className="text-3xl font-bold text-indigo-600">
              {result !== null ? result : '—'}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;